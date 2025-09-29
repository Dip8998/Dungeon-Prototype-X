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
            if (EnemyView == null || EnemyView.Controller == null) return;

            Vector3 currentPos = EnemyView.transform.position;
            Vector3 flatTarget = new Vector3(targetPosition.x, currentPos.y, targetPosition.z);

            Vector3 delta = flatTarget - currentPos;
            float dist = delta.magnitude;
            if (dist < 0.001f) return;

            Vector3 move = delta.normalized * EnemyData.moveSpeed * Time.deltaTime;
            if (move.magnitude > dist) move = delta;

            EnemyView.Controller.Move(move);
        }

        public void RotateTowards(Vector3 targetPosition)
        {
            if (EnemyView == null) return;

            Vector3 currentPos = EnemyView.transform.position;
            Vector3 flatTarget = new Vector3(targetPosition.x, currentPos.y, targetPosition.z);

            Vector3 direction = (flatTarget - currentPos);
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.0001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            EnemyView.transform.rotation = Quaternion.Slerp(
                EnemyView.transform.rotation,
                targetRotation,
                EnemyData.rotationSpeed * Time.deltaTime
            );
        }

        public Vector3 GetCurrentPatrolPoint()
        {
            if (patrolPoints == null || patrolPoints.Count == 0)
                return EnemyView.transform.position;

            return patrolPoints[currentPatrolIndex].position;
        }

        public void AdvancePatrolPoint()
        {
            if (patrolPoints == null || patrolPoints.Count == 0) return;
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }


        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }

        public void AttackPlayer()
        {
            Health playerHealth = GameService.Instance.PlayerService.GetPlayerController().Player.GetComponent<Health>();
            playerHealth.TakeDamage(10f);
        }

    }
}
