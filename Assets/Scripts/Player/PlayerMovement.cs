using SaveSystem;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private SaveGameSystem saveGameSystem;
        [SerializeField] private PlayerController inputHandle;
        [SerializeField] private PlayerCollider playerCollider;
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 5f;

        private Rigidbody2D playerRid;
        private Animator animator;

        private float speed;
        private Vector2 movement;
        private bool isMoving;
        private Vector2 moveInput;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

        private void Awake()
        {
            playerRid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            speed = walkSpeed;
            transform.position = saveGameSystem.GetPlayerPosition();
        }

        private void Update()
        {
            isMoving = inputHandle.GetMovement() != Vector2.zero;
            moveInput = inputHandle.GetMovement();
            SetMovementDirection();
            UpdateSpeed();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            playerRid.MovePosition(playerRid.position + movement * (Time.deltaTime * speed));
            if(isMoving)
                playerCollider.CheckEncounter();
        }

        private void SetMovementDirection()
        {
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                if(moveInput.x > 0)
                    movement = Vector2.right;
                if(moveInput.x < 0)
                    movement = Vector2.left;
            }
            else if (Mathf.Abs(movement.x) < Mathf.Abs(moveInput.y))
            {
                if(moveInput.y > 0)
                    movement = Vector2.up;
                if(moveInput.y < 0)
                    movement = Vector2.down;
            }
            else
                movement = Vector2.zero;
        }

        private void UpdateSpeed()
        {
            speed = inputHandle.Running() ? runSpeed : walkSpeed;
        }

        private void UpdateAnimation()
        {
            animator.SetBool(IsRunning, inputHandle.Running());
            animator.SetBool(IsMoving, isMoving);

            if (inputHandle.GetMovement() == Vector2.zero) return;
            animator.SetFloat(MoveX, movement.x);
            animator.SetFloat(MoveY, movement.y);
        }

        public bool GetMoving() => isMoving;
    }
}