using System.Collections;
using Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class PlayerHubUI : HubUI
    {
        [SerializeField] private Text currentHp;
        [SerializeField] private Text maxHp;

        public override void SetData(Pokemon pokemon)
        {
            base.SetData(pokemon);
            currentHp.text = pokemon.CurrentHp.ToString();
            maxHp.text = pokemon.MaxHp.ToString();
        }

        public override IEnumerator UpdateHealth(int valueHp)
        {
            var delta = lastCurrentHp - valueHp;
            while (valueHp != lastCurrentHp)
            {
                lastCurrentHp--;
                yield return new WaitForSeconds((float) 1/delta);
                healthBar.SetValue((float) lastCurrentHp / pokemon.MaxHp);
                currentHp.text = lastCurrentHp.ToString();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}