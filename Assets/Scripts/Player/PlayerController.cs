using DPX.ScriptableObjects;
using UnityEngine;

namespace DPX.Player
{
    public class PlayerController
    {
        private PlayerView player;
        private CharacterController controller;
        private PlayerSO playerData;

        private float hSpeed;
        private float vSpeed;

        public PlayerController(PlayerView player, PlayerSO playerSO)
        {
            this.player = player;
            playerData = playerSO;
        }

        public void StartPlayer()
        {
            controller = player.gameObject.GetComponent<CharacterController>();
        }

        public void UpdatePlayer()
        {
            hSpeed = Input.GetAxisRaw("Horizontal");
            vSpeed = Input.GetAxisRaw("Vertical");

            Vector3 moveX = player.transform.right * hSpeed * playerData.MoveSpeed * Time.deltaTime;
            Vector3 moveZ = player.transform.forward * vSpeed * playerData.MoveSpeed * Time.deltaTime;

            Vector3 move = moveX + moveZ;

            controller.Move(move);
        }
    }
}
