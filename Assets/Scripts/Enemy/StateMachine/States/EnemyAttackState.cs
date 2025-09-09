using DPX.Enemy.StateMachine;
using DPX.Enemy;
using UnityEngine;
using DPX.Interfaces;

namespace DPX.Enemies.StateMachine
{
    public class EnemyAttackState : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private EnemyController owner;
        private float attackTimer;

        public EnemyAttackState(EnemyStateMachine stateMachine, EnemyController owner)
        {
            this.stateMachine = stateMachine;
            this.owner = owner;
        }

        public void OnStateEnter()
        {
            Debug.Log("Enemy: Entering Attack State");
        }

        public void Update()
        {
            float distanceToPlayer = Vector3.Distance(owner.EnemyView.transform.position, owner.GetPlayerPosition());

            if (distanceToPlayer > owner.EnemyData.attackRange)
            {
                stateMachine.ChangeState(EnemyState.CHASE);
                return;
            }

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                owner.AttackPlayer();
                attackTimer = owner.EnemyData.attackCooldown;
            }
        }

        public void OnStateExit()
        {
            Debug.Log("Enemy: Exiting Attack State");
        }
    }
}
