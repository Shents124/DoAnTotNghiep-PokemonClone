using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainMenuInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    public UnityEvent onClick;

    public void OnTouch(InputAction.CallbackContext value)
    {
        if(value.started)
            onClick?.Invoke();
    }
}
