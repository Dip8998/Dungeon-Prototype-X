using DPX.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace DPX.Enemy
{
    public class EnemyService
    {
        private EnemyController enemyController;

        public EnemyService(EnemyView enemyView, EnemySO enemyData)
        {
            enemyController = new EnemyController(enemyView, enemyData);
            enemyView.SetController(enemyController);
        }

        public void Init(Transform player, List<Transform> patrolPoints)
        {
            enemyController.Init(player, patrolPoints);
        }

        public void Update()
        {
            enemyController.UpdateEnemy();
        }
    }
}