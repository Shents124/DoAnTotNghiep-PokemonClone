using System;
using System.Collections.Generic;
using BattleSystems.BattleUI;
using DG.Tweening;
using Moves;
using UnityEngine;

namespace BattleSystems
{
    public class MoveHandle : MonoBehaviour
    {
        [SerializeField] private List<MoveButton> moveButtons;
        [SerializeField] private EncounterPokemonBattleSystem battleSystem;
        [SerializeField] private RectTransform rectTransform;
        
        [SerializeField] private Vector2 originPosition;
        private const float YPos = 180f;

        private void Awake()
        {
            originPosition = rectTransform.anchoredPosition;
        }

        public void SetData(List<PokemonMove> pokemonMoves)
        {
            for (int i = 0; i < moveButtons.Count; i++)
            {
                if (i < pokemonMoves.Count)
                {
                    moveButtons[i].gameObject.SetActive(true);
                    var moveName = pokemonMoves[i].GetName();
                    var type = pokemonMoves[i].GetMoveType();
                    var pp = pokemonMoves[i].GetPP;
                    moveButtons[i].SetData(i, type, moveName, pp, OnClickBtn);
                }
                else
                {
                    moveButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnClickBtn(int index)
        {
            battleSystem.PlayerPokemonAttack(index);
        }

        public void ReloadData(List<PokemonMove> pokemonMoves)
        {
            for (int i = 0; i < moveButtons.Count; i++)
            {
                if (i < pokemonMoves.Count)
                {
                    moveButtons[i].gameObject.SetActive(true);
                    var pp = pokemonMoves[i].GetPP;
                    moveButtons[i].UpdatePp(pp);
                }
                else
                {
                    moveButtons[i].gameObject.SetActive(false);
                }
            }
        }

        public void EnableMoveUI() => gameObject.SetActive(true);

        public void DisableMoveUI()
        {
            gameObject.SetActive(false);
            rectTransform.anchoredPosition = originPosition;
        }

        public void DoAnimation(float duration)
        {
            EnableMoveUI();
            rectTransform.DOAnchorPos(new Vector2(originPosition.x, YPos), duration);
        }
    }
}