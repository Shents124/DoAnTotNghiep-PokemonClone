using System;
using System.Collections.Generic;
using Pokemons;
using UnityEngine;

namespace BattleSystems.BattleUI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] private List<PokemonSlotItem> pokemonSlotItems;
        private List<Pokemon> pokemons;
        private Action<Pokemon> onSwitchPokemon;
        private Action onClickBackBtn;
        public void SetData(List<Pokemon> pokemons, Action<Pokemon> onSwitchPokemon, Action onClickBackBtn)
        {
            this.pokemons = pokemons;
            this.onSwitchPokemon = onSwitchPokemon;
            this.onClickBackBtn = onClickBackBtn;
            for (var i = 0; i < pokemonSlotItems.Count; i++)
            {
                if(i < pokemons.Count)
                    pokemonSlotItems[i].SetData(pokemons[i], onSwitchPokemon);
                else
                {
                    pokemonSlotItems[i].SetStateButton(false);
                }
            }
        }

        public void ReloadData()
        {
            foreach (var item in pokemonSlotItems)
            {
                item.ReloadData();
            }
        }

        public void OnClickBackBtn()
        {
            onClickBackBtn?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
