using DPX.Enemy.StateMachine;
using DPX.Enemy;
using DPX.Interfaces;
using UnityEngine;

namespace DPX.Enemies.StateMachine
{
    public class EnemyChaseState : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private EnemyController owner;

        public EnemyChaseState(EnemyStateMachine stateMachine, EnemyController owner)
        {
            this.stateMachine = stateMachine;
            this.owner = owner;
        }

        public void OnStateEnter()
        {
            Debug.Log("Enemy: Entering Chase State");
        }

        public void Update()
        {
            Vector3 playerPosition = owner.GetPlayerPosition();
            owner.RotateTowards(playerPosition);
            owner.MoveTo(playerPosition);

            float distanceToPlayer = Vector3.Distance(owner.EnemyView.transform.position, playerPosition);
            if (distanceToPlayer <= owner.EnemyData.attackRange)
            {
                stateMachine.ChangeState(EnemyState.ATTACK);
            }
            else if (distanceToPlayer > owner.EnemyData.chaseDistance)
            {
                stateMachine.ChangeState(EnemyState.IDLE);
            }
        }

        public void OnStateExit()
        {
            Debug.Log("Enemy: Exiting Chase State");
        }
    }
}
