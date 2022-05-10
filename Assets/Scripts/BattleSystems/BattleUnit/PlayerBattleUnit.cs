using System;
using System.Collections;
using BattleSystems.BattleUI;
using DG.Tweening;
using Pokemons;
using UnityEngine;

namespace BattleSystems.BattleUnit
{
    public class PlayerBattleUnit : BaseBattleUnit
    {
        [SerializeField] private TrainerUIController trainerUiController;
        [SerializeField] private RectTransform trainer;
        private Vector2 trainerPos;
        private static readonly int InitBattle = Animator.StringToHash("InitBattle");
        private const float XFliedPos = 380f;
        private const float XPos = -1000f;

        public void SetData(Pokemon pokemon, Dialog dialog, Action onCatch)
        {
            SetData(pokemon, dialog);
            trainerUiController.SetOnCatchPokemonEvent(onCatch);
        }
        
        public override void SetData(Pokemon pokemon, Dialog dialog)
        {
            base.SetData(pokemon, dialog);
            pokemonImage.sprite = pokemon.BasePokemon.BackSprite;
            trainerPos = trainer.anchoredPosition;
        }

        public override IEnumerator InitFlied()
        {
            pokemonRectTransform.gameObject.SetActive(false);
            hubUi.gameObject.SetActive(false);
            flied.DOAnchorPos(new Vector2(XFliedPos, fliedPos.y), animationDelay);
            yield return new WaitForSeconds(animationDelay);
            pokemonRectTransform.anchoredPosition = new Vector2(XPos, pokemonPos.y);
        }

        public override IEnumerator PlayStartBattleAnimation()
        {
            var animator = trainer.gameObject.GetComponent<Animator>();
            StartCoroutine(dialog.SetDialogText($"Go {pokemon.BasePokemon.PokemonName}! "));
            animator.SetTrigger(InitBattle);
            trainer.DOAnchorPos(new Vector2(XPos, trainerPos.y), animationDelay).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(animationDelay / 2);
            
            pokemonRectTransform.gameObject.SetActive(true);
            pokemonRectTransform.DOAnchorPos(pokemonPos, animationDelay / 2);
            hubUi.gameObject.SetActive(true);
            yield return new WaitForSeconds(animationDelay);
        }

        public override IEnumerator PlayAttackAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(pokemonRectTransform.DOAnchorPos(new Vector2(pokemonPos.x + attackDistance, pokemonPos.y),
                animationDelay / 8));
            sequence.Append(pokemonRectTransform.DOAnchorPos(new Vector2(pokemonPos.x, pokemonPos.y),
                animationDelay / 8));
            yield return new WaitForSeconds(animationDelay / 4);
        }

        public override IEnumerator PlaySwitchPokemon(Pokemon pokemonSwitch)
        {
            if (pokemon.CurrentHp > 0)
            {
                yield return dialog.SetDialogText($"Come on back, {pokemon.BasePokemon.PokemonName}");
                pokemonRectTransform.DOAnchorPos(new Vector2(XPos, pokemonPos.y), animationDelay).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(animationDelay);
            }
            pokemon = pokemonSwitch;
            pokemonRectTransform.localScale = Vector3.one;
            pokemonRectTransform.anchoredPosition = new Vector2(XPos, pokemonPos.y);
            pokemonImage.sprite = pokemon.BasePokemon.BackSprite;
            StartCoroutine(dialog.SetDialogText($"Go {pokemon.BasePokemon.PokemonName}! "));
            pokemonRectTransform.DOAnchorPos(pokemonPos, animationDelay / 2);
            yield return new WaitForSeconds(animationDelay / 2);
            hubUi.gameObject.SetActive(true);
            hubUi.SetData(pokemon);
        }

        public void CatchPokemonFail()
        {
            trainerUiController.BrokeBall();
        }
    }
}