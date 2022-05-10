using System.Collections.Generic;
using Pokemons;
using UnityEngine;

namespace Map
{
    public class GrassMapArea : MonoBehaviour
    {
        [SerializeField] private List<BasePokemon> wildPokemon;
        [SerializeField] private int minLevel;
        [SerializeField] private int maxLevel;
        
        public Pokemon GetRandomWildPokemon()
        {
            var randomLevel = Random.Range(minLevel, maxLevel);
            var randomPokemon = Random.Range(0, wildPokemon.Count);
            var basePokemon = wildPokemon[randomPokemon];

            var pokemon = new Pokemon(basePokemon, randomLevel);
            pokemon.InitBaseMove();
            return pokemon;
        }
    }
}
