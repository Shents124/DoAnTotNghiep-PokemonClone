using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput playerInput;

        private Vector2 movement;
        private bool isRunning;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            movement = value.ReadValue<Vector2>();
        }

        public void OnRunning(InputAction.CallbackContext value)
        {
            isRunning = value.ReadValueAsButton();
        }

        public Vector2 GetMovement() => movement;
    
        public bool Running() => isRunning;

        public void SetInputActionState(bool isPause)
        {
            if (isPause)
                playerInput.DeactivateInput();
            else
                playerInput.ActivateInput();
        }
    }
}