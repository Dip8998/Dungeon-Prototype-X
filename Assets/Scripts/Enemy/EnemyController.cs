using DPX.Enemy.StateMachine;
using DPX.GameHealth;
using DPX.Main;
using DPX.Player;
using DPX.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace DPX.Enemy
{
    public class EnemyController
    {
        public EnemyView EnemyView { get; private set; }
        public EnemySO EnemyData { get; private set; }
        public EnemyStateMachine StateMachine { get; private set; }


        private Transform playerTransform;

        private List<Transform> patrolPoints;
        private int currentPatrolIndex = 0;

        public EnemyController(EnemyView enemyView, EnemySO enemyData)
        {
            this.EnemyView = enemyView;
            this.EnemyData = enemyData;
            this.StateMachine = new EnemyStateMachine(this);
        }

        public void Init(Transform player, List<Transform> patrolPoints)
        {
            this.playerTransform = player;
            this.patrolPoints = patrolPoints;
            StateMachine.ChangeState(EnemyState.IDLE);
        }

        public void UpdateEnemy()
        {
            StateMachine.Update();
        }

        public void MoveTo(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - EnemyView.transform.position).normalized;
            EnemyView.Controller.Move(direction * EnemyData.moveSpeed * Time.deltaTime);
        }

        public void RotateTowards(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - EnemyView.transform.position).normalized;
            direction.y = 0; 
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                EnemyView.transform.rotation = Quaternion.Slerp(
                    EnemyView.transform.rotation,
                    targetRotation,
                    EnemyData.rotationSpeed * Time.deltaTime
                );
            }
        }

        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }

        public Vector3 GetNextPatrolPoint()
        {
            if (patrolPoints == null || patrolPoints.Count == 0)
            {
                return EnemyView.transform.position;
            }

            Vector3 targetPoint = patrolPoints[currentPatrolIndex].position;

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;

            return targetPoint;
        }

        public void AttackPlayer()
        {
            Health playerHealth = GameService.Instance.PlayerService.GetPlayerController().Player.GetComponent<Health>();
            playerHealth.TakeDamage(10f);
        }
    }
}
