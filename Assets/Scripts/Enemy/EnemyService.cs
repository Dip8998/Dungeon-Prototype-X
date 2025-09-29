using DPX.Enemy.StateMachine;
using DPX.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace DPX.Enemy
{
    public class EnemyService
    {
        private readonly List<EnemyController> enemyControllers = new List<EnemyController>();

        public EnemyService() { }

        public void RegisterEnemy(EnemyView enemyView, EnemySO enemyData)
        {
            if (enemyView == null) return;

            EnemyController controller = new EnemyController(enemyView, enemyData);
            enemyView.SetController(controller);
            enemyControllers.Add(controller);
        }

        public void InitAll(Transform player, List<Transform> patrolPoints)
        {
            foreach (var controller in enemyControllers)
            {
                if (controller?.EnemyView != null)
                    controller.Init(player, patrolPoints);
            }
        }

        public void UpdateAll()
        {
            for (int i = enemyControllers.Count - 1; i >= 0; i--)
            {
                var controller = enemyControllers[i];

                if (controller == null || controller.EnemyView == null)
                {
                    enemyControllers.RemoveAt(i);
                    continue;
                }

                controller.UpdateEnemy();
            }
        }
    }
}
