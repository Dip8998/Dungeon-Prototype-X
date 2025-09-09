using UnityEngine;
using DPX.Interfaces;
using DPX.Enemy.StateMachine;
using DPX.Enemy;

namespace DPX.Enemies.StateMachine
{
    public class EnemyIdleState : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private EnemyController owner;
        private Vector3 currentPatrolTarget;
        private float patrolTolerance = 0.5f; 

        public EnemyIdleState(EnemyStateMachine stateMachine, EnemyController owner)
        {
            this.stateMachine = stateMachine;
            this.owner = owner;
        }

        public void OnStateEnter()
        {
            Debug.Log("Enemy: Entering Idle State (Patrolling)");
            currentPatrolTarget = owner.GetNextPatrolPoint();
        }

        public void Update()
        {
            float distanceToPlayer = Vector3.Distance(owner.EnemyView.transform.position, owner.GetPlayerPosition());
            if (distanceToPlayer <= owner.EnemyData.chaseDistance)
            {
                stateMachine.ChangeState(EnemyState.CHASE);
                return;
            }

            float distanceToTarget = Vector3.Distance(owner.EnemyView.transform.position, currentPatrolTarget);
            if (distanceToTarget > patrolTolerance)
            {
                owner.RotateTowards(currentPatrolTarget);
                owner.MoveTo(currentPatrolTarget);
            }
            else
            {
                currentPatrolTarget = owner.GetNextPatrolPoint();
            }
        }

        public void OnStateExit()
        {
            Debug.Log("Enemy: Exiting Idle State");
        }
    }
}
