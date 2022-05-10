using System.Collections;
using Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class HubUI : MonoBehaviour
    {
        [SerializeField] private Text lbPokemonName;
        [SerializeField] private Text lbLevel;
        [SerializeField] protected HealthBar healthBar;
        protected Pokemon pokemon;
        protected int lastCurrentHp;

        public virtual void SetData(Pokemon pokemon)
        {
            this.pokemon = pokemon;
            lbPokemonName.text = pokemon.BasePokemon.PokemonName;
            lbLevel.text = pokemon.Level.ToString();
            healthBar.SetValue((float) pokemon.CurrentHp / pokemon.MaxHp);
            lastCurrentHp = pokemon.CurrentHp;
        }

        public virtual IEnumerator UpdateHealth(int valueHp)
        {
            var delta = lastCurrentHp - valueHp;
            while (valueHp != lastCurrentHp)
            {
                lastCurrentHp--;
                yield return new WaitForSeconds((float) 1 / delta);
                healthBar.SetValue((float) lastCurrentHp / pokemon.MaxHp);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}