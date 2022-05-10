using System;
using System.Collections;
using BattleSystems.BattleUnit;
using DG.Tweening;
using UnityEngine;

namespace BattleSystems.BattleUI
{
    public class TrainerUIController : MonoBehaviour
    {
        [SerializeField] private EncounterPokemonBattleUnit encounterPokemon;
        [SerializeField] private RectTransform trainerRect;
        [SerializeField] private RectTransform pokemonBallRect;
        [SerializeField] private float throwPokemonBallDur = 2f;
        [SerializeField] private float fadeTrainerDur = 0.5f;
        
        private Animator animator;
        private readonly Vector2 goalPos = new Vector2(1290, 450);
        private static readonly int Pokemon = Animator.StringToHash("CatchPokemon");
        private EncounterPokemonBattleSystem battleSystem;
        private Vector2 originBallPos;

        private Action onCatchPokemon;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            battleSystem = GetComponentInParent<EncounterPokemonBattleSystem>();
            originBallPos = pokemonBallRect.anchoredPosition;
        }

        public void SetOnCatchPokemonEvent(Action onCatch)
        {
            onCatchPokemon = onCatch;
        }
        
        private IEnumerator CatchPokemon()
        {
            battleSystem.dialog.gameObject.SetActive(false);
            trainerRect.DOAnchorPos(new Vector2(-270, trainerRect.anchoredPosition.y), fadeTrainerDur);
            yield return new WaitForSeconds(fadeTrainerDur);
            animator.SetTrigger(Pokemon);
        }

        public void ThrowPokemonBall()
        {
            StartCoroutine(CatchPokemon());
        }

        private IEnumerator ThrowPokemonBallCoroutine()
        {
            SoundManager.Instance.PlayThrowBall();
            pokemonBallRect.gameObject.SetActive(true);
            pokemonBallRect.DOLocalRotate(new Vector3(0, 0, 360), 1f);
            pokemonBallRect.DOAnchorPos(goalPos, throwPokemonBallDur).OnComplete(StartCatchPokemonCoroutine);
            yield return new WaitForSeconds(throwPokemonBallDur);
        }

        private void StartCatchPokemonCoroutine()
        {
            StartCoroutine(CatchPokemonCoroutine());
        }

        private IEnumerator CatchPokemonCoroutine()
        {
            yield return encounterPokemon.CatchedPokemon();
            pokemonBallRect.DOAnchorPos(new Vector2(goalPos.x, 270), 0.2f);
            onCatchPokemon?.Invoke();
        }

        public void BrokeBall()
        {
            pokemonBallRect.gameObject.SetActive(false);
            pokemonBallRect.anchoredPosition = originBallPos;
        }
    }
}