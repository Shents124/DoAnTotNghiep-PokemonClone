using System.Collections.Generic;
using Pokemons;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "BasePokemonStorage", menuName = "Save System/BasePokemonStorage")]
    public class BasePokemonStorage: ScriptableObject
    {
        [SerializeField] private List<BasePokemon> listPokemon;

        private readonly Dictionary<int, BasePokemon> pokemonDic = new Dictionary<int, BasePokemon>();
        
        public void LoadBasePokemonData()
        {
            foreach (var pokemon in listPokemon)
            {
                pokemonDic.Add(pokemon.Id, pokemon);
            }
        }

        public BasePokemon GetBasePokemonById(int id)
        {
            pokemonDic.TryGetValue(id, out var basePokemon);
            return basePokemon;
        }
    }
}