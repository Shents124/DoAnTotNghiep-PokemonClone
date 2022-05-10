using System.Collections.Generic;
using UnityEngine;

namespace Pokemons
{
    [CreateAssetMenu(fileName = "PokemonPcSO", menuName = "Save System/PokemonPcSO")]
    public class PokemonPCSO: ScriptableObject
    {
        private List<Pokemon> listPokemon;

        public List<Pokemon> ListPokemon
        {
            get
            {
                if(listPokemon == null)
                    listPokemon = new List<Pokemon>();
                return listPokemon;
            }
        }
        public void Init()
        {
            if(listPokemon == null)
                listPokemon = new List<Pokemon>();
            listPokemon.Clear();
        }
        
        public void InitPokemon(BasePokemon basePokemon, int level)
        {
            var pokemon = new Pokemon(basePokemon, level);
            listPokemon.Add(pokemon);
        }

        public void AddPokemon(Pokemon pokemon)
        {
            if(pokemon== null)
                return;
            listPokemon.Add(pokemon);
        }

        public List<Pokemon> GetListPokemon() => listPokemon;
    }
}