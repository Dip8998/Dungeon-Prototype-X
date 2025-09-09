using DPX.Player;
using UnityEngine;

namespace DPX.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController controller;

        public void SetController(EnemyController playerController)
        {
            this.controller = playerController;
        }
    }
}
