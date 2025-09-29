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
        private const float patrolTolerance = 0.5f;

        public EnemyIdleState(EnemyStateMachine stateMachine, EnemyController owner)
        {
            this.stateMachine = stateMachine;
            this.owner = owner;
        }

        public void OnStateEnter()
        {
            currentPatrolTarget = owner.GetCurrentPatrolPoint();
        }

        public void Update()
        {
            if (owner == null || owner.EnemyView == null) return;

            float distanceToPlayer = Vector3.Distance(owner.EnemyView.transform.position, owner.GetPlayerPosition());
            if (distanceToPlayer <= owner.EnemyData.chaseDistance)
            {
                stateMachine.ChangeState(EnemyState.CHASE);
                return;
            }

            Vector3 flatEnemy = new Vector3(owner.EnemyView.transform.position.x, 0f, owner.EnemyView.transform.position.z);
            Vector3 flatTarget = new Vector3(currentPatrolTarget.x, 0f, currentPatrolTarget.z);
            float distanceToTarget = Vector3.Distance(flatEnemy, flatTarget);

            if (distanceToTarget > patrolTolerance)
            {
                owner.RotateTowards(currentPatrolTarget);
                owner.MoveTo(currentPatrolTarget);
            }
            else
            {
                owner.AdvancePatrolPoint();
                currentPatrolTarget = owner.GetCurrentPatrolPoint();
            }
        }

        public void OnStateExit()
        {
        }
    }
}
