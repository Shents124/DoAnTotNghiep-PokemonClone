using System.Collections;
using BattleSystems.BattleUI;
using DG.Tweening;
using Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUnit
{
    public abstract class BaseBattleUnit : MonoBehaviour
    {
        [SerializeField] protected Image pokemonImage;
        [SerializeField] protected RectTransform pokemonRectTransform;
        [SerializeField] protected HubUI hubUi;
        [SerializeField] protected RectTransform flied;
        [SerializeField] protected float animationDelay = 1f;
        protected Pokemon pokemon;
        protected Dialog dialog;
        protected Vector2 pokemonPos;
        protected Vector2 fliedPos;
        protected float attackDistance = 50f;
        
        public virtual void SetData(Pokemon pokemon, Dialog dialog)
        {
            this.pokemon = pokemon;
            hubUi.SetData(pokemon);
            this.dialog = dialog;
            pokemonPos = pokemonRectTransform.anchoredPosition;
            fliedPos = flied.anchoredPosition;
        }

        public IEnumerator UpdateHealth()
        {
            yield return hubUi.UpdateHealth(pokemon.CurrentHp);
        }

        public virtual IEnumerator PlayAttackAnimation()
        {
            yield break;
        }
        public virtual IEnumerator InitFlied()
        {
            yield break;
        }
        public virtual IEnumerator PlayStartBattleAnimation()
        {
            yield break;
        }

        public virtual IEnumerator PlayPokemonFaintedAnimation()
        {
            pokemonRectTransform.DOScale(Vector3.zero, animationDelay / 2);
            yield return new WaitForSeconds(animationDelay / 2);
            hubUi.gameObject.SetActive(false);
        }

        public virtual IEnumerator PlaySwitchPokemon(Pokemon pokemonSwitch)
        {
            yield break;
        }

        public virtual IEnumerator PlayNextPokemon()
        {
            yield break;
        }
    }
}
