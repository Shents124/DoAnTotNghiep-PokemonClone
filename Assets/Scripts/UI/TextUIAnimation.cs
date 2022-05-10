using DG.Tweening;
using UnityEngine;

public class TextUIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private void Start()
    {
        DoAnimation();
    }

    private void OnDestroy()
    {
        DOTween.KillAll(this);
    }

    private void DoAnimation()
    {
        DoMinimize();
    }

    private void DoMinimize()
    {
        rectTransform.DOScale(new Vector3(0.8f, 0.8f), 1f).OnComplete(DoMaximize);
    }

    private void DoMaximize()
    {
        rectTransform.DOScale(new Vector3(1.1f, 1.1f), 1f).OnComplete(DoMinimize);
    }
}