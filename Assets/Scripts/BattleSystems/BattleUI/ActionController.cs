using DG.Tweening;
using UnityEngine;

namespace BattleSystems.BattleUI
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private MoveHandle moveHandle;
        [SerializeField] private PartyUI partyUi;
        [SerializeField] private float duration = 1f;
        [SerializeField] private RectTransform actionControllerRect;
        private Vector2 originPosition;
        private const float DeltaX = 300f;

        private void Awake()
        {
            originPosition = actionControllerRect.anchoredPosition;
        }

        public void OnClickAttackBtn()
        {
            actionControllerRect.DOAnchorPos(new Vector2(DeltaX, originPosition.y), duration);
            moveHandle.DoAnimation(duration);
        }

        public void OnClickPokemonParty()
        {
            gameObject.SetActive(false);
            actionControllerRect.DOAnchorPos(new Vector2(DeltaX, originPosition.y), duration);
        }
        
        public void EnableUi()
        {
            if(gameObject.activeInHierarchy == false)
                gameObject.SetActive(true);
            actionControllerRect.DOAnchorPos(originPosition, duration);
        }
    }
}
