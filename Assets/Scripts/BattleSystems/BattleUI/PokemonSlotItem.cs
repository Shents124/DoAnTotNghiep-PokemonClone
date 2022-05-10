using System;
using Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class PokemonSlotItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject content;
        [SerializeField] private Image icon;
        [SerializeField] private Text pokemonName;
        [SerializeField] private Text lbLevel;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Text lbCurrentHp;
        [SerializeField] private Text lbMaxHp;
        
        private Pokemon pokemon;
        private Action<Pokemon> onSwitchPokemon;
        public void SetData(Pokemon pokemonData, Action<Pokemon> onSwitchPokemon)
        {
            if(pokemonData == null) return;
            this.pokemon = pokemonData;
            this.onSwitchPokemon = onSwitchPokemon;
            icon.sprite = pokemonData.BasePokemon.Icon;
            pokemonName.text = pokemonData.BasePokemon.PokemonName;
            lbLevel.text = pokemon.Level.ToString();
            healthBar.SetValue((float) pokemonData.CurrentHp/pokemonData.MaxHp);
            lbCurrentHp.text = pokemonData.CurrentHp.ToString();
            lbMaxHp.text = pokemonData.MaxHp.ToString();
            CheckPokemonIsFainted();
        }

        public void ReloadData()
        {
            if(pokemon == null) return;
            healthBar.SetValue((float) pokemon.CurrentHp/pokemon.MaxHp);
            lbCurrentHp.text = pokemon.CurrentHp.ToString();
            lbMaxHp.text = pokemon.MaxHp.ToString();
            CheckPokemonIsFainted();
        }
        
        public void SetStateButton(bool value = true)
        {
            button.interactable = value;
            content.SetActive(value);
        }

        public void OnSwitchPokemon()
        {
            onSwitchPokemon?.Invoke(pokemon);
        }

        private void CheckPokemonIsFainted()
        {
            if (pokemon.CurrentHp <= 0)
                button.interactable = false;
        }
    }
}
