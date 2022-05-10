using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace BattleSystems.BattleUI
{
    public class PokemonFaintedUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animDuration;
        private Vector2 originPos;

        private void OnEnable()
        {
            originPos = rectTransform.anchoredPosition;
        }

        public IEnumerator DisplaySelections()
        {
            gameObject.SetActive(true);
            rectTransform.DOAnchorPos(new Vector2(-270, originPos.y), animDuration);
            yield return new WaitForSeconds(animDuration);
        }

        public void OnClickSwitchPokemonBtn()
        {
            gameObject.SetActive(false);
            rectTransform.anchoredPosition = originPos;
        }
    }
}
