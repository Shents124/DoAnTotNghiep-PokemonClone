using System;
using System.Collections.Generic;
using GameEvents;
using SaveSystem;
using UnityEngine;

namespace Pokemons
{
    public class PokemonPartyManager : MonoBehaviour
    {
        [SerializeField] private SaveGameSystem saveGameSystem;
        [SerializeField] private PokemonPartySO pokemonPartySo;
        [SerializeField] private PokemonPCSO pokemonPcSo;
        [SerializeField] private PokemonEventSO addPokemonEventSo;

        private void OnEnable()
        {
            addPokemonEventSo.OnRasiedEvent += AddPokemon;
        }

        private void OnDisable()
        {
            addPokemonEventSo.OnRasiedEvent -= AddPokemon;
        }

        private void AddPokemon(Pokemon pokemon, Action<bool> callback)
        {
            var listPokemon = pokemonPartySo.GetListPartyPokemon();
            if (listPokemon.Count >= 6)
            {
                pokemonPcSo.AddPokemon(pokemon);
                callback.Invoke(false);
            }
            else
            {
                pokemonPartySo.AddPokemon(pokemon);
                callback.Invoke(true);
            }
            
            saveGameSystem.SaveDataToDisk();
        }

        private void MovePokemonToPc(Pokemon pokemon)
        {
            var listPokemon = pokemonPartySo.GetListPartyPokemon();
            if (!listPokemon.Contains(pokemon)) return;

            pokemon.Recover();
            pokemonPcSo.AddPokemon(pokemon);
            pokemonPartySo.RemovePokemon(pokemon);
        }

        public List<Pokemon> GetListPokemonParty()
        {
            return pokemonPartySo.GetListPartyPokemon();
        }
    }
}