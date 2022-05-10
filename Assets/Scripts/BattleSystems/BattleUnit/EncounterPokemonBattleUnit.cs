using System.Collections;
using BattleSystems.BattleUI;
using DG.Tweening;
using Pokemons;
using UnityEngine;

namespace BattleSystems.BattleUnit
{
    public class EncounterPokemonBattleUnit : BaseBattleUnit
    {
        public override void SetData(Pokemon pokemon, Dialog dialog)
        {
            base.SetData(pokemon, dialog);
            pokemonImage.sprite = pokemon.BasePokemon.FrontSprite;
        }

        public override IEnumerator InitFlied()
        {
            hubUi.gameObject.SetActive(false);
            flied.DOAnchorPos(new Vector2(-700f, fliedPos.y), animationDelay);
            yield return new WaitForSeconds(animationDelay);
            hubUi.gameObject.SetActive(true);
        }

        public override IEnumerator PlayAttackAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(pokemonRectTransform.DOAnchorPos(new Vector2(pokemonPos.x - attackDistance, pokemonPos.y),
                animationDelay / 8));
            sequence.Append(pokemonRectTransform.DOAnchorPos(new Vector2(pokemonPos.x, pokemonPos.y),
                animationDelay / 8));
            yield return new WaitForSeconds(animationDelay / 4);
        }

        public IEnumerator CatchedPokemon()
        {
            pokemonRectTransform.DOScale(Vector3.zero, animationDelay / 2);
            pokemonRectTransform.DOAnchorPos(new Vector2(-50, 255), animationDelay / 2);
            yield return new WaitForSeconds(animationDelay);
        }

        public IEnumerator BrokePokemonBall()
        {
            pokemonRectTransform.DOScale(Vector3.one, animationDelay / 2);
            pokemonRectTransform.DOAnchorPos(pokemonPos, animationDelay / 2);
            yield return new WaitForSeconds(animationDelay);
        }
    }
}