using DPX.InputSystem;
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

        private Vector3 dir;
        private float currentVelocity;
        private float velocity;

        public PlayerController(PlayerView player, PlayerSO playerSO)
        {
            this.player = player;
            playerData = playerSO;
            inputs = new InputHandler();
        }

        public void StartPlayer()
        {
            controller = player.gameObject.GetComponent<CharacterController>();
        }

        public void UpdatePlayer()
        {
            inputs.HandleInput();

            PlayerGravity();
            PlayerRotation();
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            Vector3 horizontal = inputs.MovementInput * playerData.MoveSpeed;
            Vector3 vertical = new Vector3(0, velocity, 0);

            Vector3 finalMove = (horizontal + vertical) * Time.deltaTime;

            controller.Move(finalMove);
        }


        private void PlayerRotation()
        {
            if (dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref currentVelocity, playerData.SmoothRotation);
                player.transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }

        private void PlayerGravity()
        {
            if(controller.isGrounded && velocity < 0)
            {
                velocity = -1f;
            }
            else
            {
                velocity += playerData.Gravity * playerData.GravityMultiplyer * Time.deltaTime;
            }
            dir.y = velocity;
        }
    }
}
