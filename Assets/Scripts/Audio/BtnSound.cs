using UnityEngine;

public class BtnSound : MonoBehaviour
{
    public void OnBtnClick()
    {
        SoundManager.Instance.PlaySelectButton();
    }
}
