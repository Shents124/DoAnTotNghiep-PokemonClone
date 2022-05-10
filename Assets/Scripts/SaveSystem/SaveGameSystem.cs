using System.Collections.Generic;
using Moves;
using Pokemons;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "Save System", menuName = "Save System/Save System")]
    public class SaveGameSystem : ScriptableObject
    {
        [SerializeField] private BasePokemonStorage basePokemonStorage;
        [SerializeField] private BaseMoveStorage baseMoveStorage;
        [SerializeField] private PokemonPartySO pokemonPartySo;
        [SerializeField] private PokemonPCSO pokemonPcSo;
        [SerializeField] private PlayerPositionSO playerPositionSo;

        private const string SaveFileName = "save.pokemon";
        private Save saveData;

        public void LoadBaseData()
        {
            basePokemonStorage.LoadBasePokemonData();
            baseMoveStorage.LoadBaseMoveData();
        }

        public bool LoadSaveDataFromDisk()
        {
            saveData = new Save();
            
            if (!FileManager.LoadFromFile(SaveFileName, out var json)) return false;
            if (string.IsNullOrEmpty(json))
                return false;
            saveData.LoadFormJson(json);
            playerPositionSo.SetPos(saveData.xLocation, saveData.yLocation);
            return true;
        }

        public Vector2 GetPlayerPosition()
        {
            return playerPositionSo.GetPos();
        }

        public void LoadPlayerPokemon()
        {
            LoadPokemonParty();
            LoadPokemonPc();
        }
        
        private void LoadPokemonPc()
        {
            pokemonPcSo.Init();
            
            foreach (var item in saveData.pokemonPcStack)
            {
                var basePokemon = basePokemonStorage.GetBasePokemonById(item.id);
                var pokemonMove = LoadMoveData(item);
                pokemonPcSo.InitPokemon(basePokemon, item.level);
            }
        }
        
        private void LoadPokemonParty()
        {
            pokemonPartySo.Init();
            foreach (var item in saveData.pokemonPartyStack)
            {
                var basePokemon = basePokemonStorage.GetBasePokemonById(item.id);
                var pokemonMove = LoadMoveData(item);
                pokemonPartySo.InitPokemon(basePokemon, pokemonMove, item.level, item.currentHp);
            }
        }

        private List<PokemonMove> LoadMoveData(SerializedPokemonData serializedPokemonData)
        {
            var pokemonMoves = new List<PokemonMove>();
            foreach (var move in serializedPokemonData.moveData)
            {
                var baseMove = baseMoveStorage.GetBaseMoveById(move.id);
                pokemonMoves.Add(new PokemonMove(baseMove, move.pp));
            }

            return pokemonMoves;
        }

        private static void SaveListPokemon(ref List<SerializedPokemonData> stackPokemon, IEnumerable<Pokemon> listPokemon)
        {
            stackPokemon.Clear();
            foreach (var item in listPokemon)
            {
                var serializedData = new SerializedPokemonData(item);
                stackPokemon.Add(serializedData);
            }
        }
        public void SaveDataToDisk()
        {
            SaveListPokemon(ref saveData.pokemonPartyStack, pokemonPartySo.GetListPartyPokemon());
            SaveListPokemon(ref saveData.pokemonPcStack, pokemonPcSo.GetListPokemon());
            
            saveData.xLocation = playerPositionSo.xPos;
            saveData.yLocation = playerPositionSo.yPos;

            if (FileManager.WriteToFile(SaveFileName, saveData.ToJson()))
            {
                Debug.Log("Save successful " + SaveFileName);
            }
        }

        public void InitFirstPlayGame()
        {
            pokemonPartySo.Init();
            var basePokemon = basePokemonStorage.GetBasePokemonById(157);
            pokemonPartySo.InitFirstPokemon(basePokemon,95);
            saveData.xLocation = 0;
            saveData.yLocation = 0;
            SaveDataToDisk();
        }
    }
}