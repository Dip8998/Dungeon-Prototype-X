using DPX.Inputs;
using DPX.ScriptableObjects;
using UnityEngine;

namespace DPX.Player
{
    public class PlayerController
    {
        private PlayerView player;
        private CharacterController controller;
        private InputHandler inputs;
        private PlayerSO playerData;

        private Vector3 velocity;
        private float currentVelocity;
        private Camera cam;

        public PlayerController(PlayerView player, PlayerSO playerSO)
        {
            this.player = player;
            playerData = playerSO;
            inputs = new InputHandler();
            cam = Camera.main;
        }

        public void StartPlayer()
        {
            controller = player.gameObject.GetComponent<CharacterController>();
        }

        public void UpdatePlayer()
        {
            inputs.UpdateInput();

            HandleMovement();
            ApplyGravity();
        }

        private void HandleMovement()
        {
            Quaternion yawRotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
            Vector3 camForward = yawRotation * Vector3.forward;
            Vector3 camRight = yawRotation * Vector3.right;

            Vector3 moveDir = (camForward * inputs.MoveInput.z + camRight * inputs.MoveInput.x).normalized;

            float targetSpeed = inputs.SprintInput ? playerData.SprintSpeed : playerData.MoveSpeed;

            Vector3 move = moveDir * targetSpeed;
            controller.Move((move + Vector3.up * velocity.y) * Time.deltaTime);

            if (moveDir.sqrMagnitude > 0.01f)
            {
                HandleRotation(moveDir);
            }
        }

        private void HandleRotation(Vector3 moveDir)
        {
            if (moveDir.sqrMagnitude < 0.001f) return;

            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(
                player.transform.eulerAngles.y,
                targetAngle,
                ref currentVelocity,
                playerData.RotationSpeed
            );

            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        private void ApplyGravity()
        {
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -1f; 
            else
                velocity.y += playerData.Gravity * playerData.GravityMultiplyer * Time.deltaTime;
        }
    }
}
