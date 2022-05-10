using System.Collections.Generic;
using System.Linq;
using Moves;
using UnityEngine;

namespace Pokemons
{
    [CreateAssetMenu(fileName = "PokemonPartySO", menuName = "Save System/PokemonPartySO")]
    public class PokemonPartySO : ScriptableObject
    {
        [SerializeField] private PokemonPCSO pokemonPcSo;
        [SerializeField] private List<Pokemon> listPokemon = new List<Pokemon>();
        
        public void Init()
        {
            if (listPokemon == null)
                listPokemon = new List<Pokemon>();
            listPokemon.Clear();
        }

        public void InitPokemon(BasePokemon basePokemon, List<PokemonMove> move, int level, int currentHp)
        {
            if (listPokemon.Count > 6) return;
            var pokemon = new Pokemon(basePokemon, level);
            pokemon.SetMove(move);
            pokemon.SetCurrentHp(currentHp);
            listPokemon.Add(pokemon);
        }

        public void InitFirstPokemon(BasePokemon basePokemon, int level)
        {
            var pokemon = new Pokemon(basePokemon, level);
            pokemon.InitBaseMove();
            listPokemon.Add(pokemon);
        }

        public List<Pokemon> GetListPartyPokemon()
        {
            if (listPokemon == null)
                listPokemon = new List<Pokemon>();

            if (IsCanRecover())
                RecoverPokemon();
            return listPokemon;
        }

        public void AddPokemon(Pokemon pokemon)
        {
            if (pokemon == null)
                return;
            listPokemon.Add(pokemon);
        }

        public void RemovePokemon(Pokemon pokemon)
        {
            listPokemon.Remove(pokemon);
        }

        private void RecoverPokemon()
        {
            foreach (var pokemon in listPokemon)
            {
                pokemon.Recover();
            }
        }

        private bool IsCanRecover()
        {
            return listPokemon.Any(pokemon => pokemon.CurrentHp <= 0);
        }
    }
}