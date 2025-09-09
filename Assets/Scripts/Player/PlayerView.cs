using DPX.ScriptableObjects;
using DPX.Weapons;
using UnityEngine;

namespace DPX.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController controller;

        public void SetController(PlayerController playerController)
        {
            this.controller = playerController;
        }
    }
}