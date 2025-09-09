using DPX.Enemies.StateMachine;
using DPX.Interfaces;
using System.Collections.Generic;

namespace DPX.Enemy.StateMachine
{
    public class EnemyStateMachine
    {
        private IEnemyState currentState;
        private Dictionary<EnemyState, IEnemyState> States = new Dictionary<EnemyState, IEnemyState>();
        private EnemyController owner;

        public EnemyStateMachine(EnemyController owner)
        {
            this.owner = owner;

            CreateState();
        }

        private void CreateState()
        {
            States.Add(EnemyState.IDLE, new EnemyIdleState(this, owner));
            States.Add(EnemyState.CHASE, new EnemyChaseState(this, owner));
            States.Add(EnemyState.ATTACK, new EnemyAttackState(this, owner));
            States.Add(EnemyState.DEAD, new EnemyDeadState(this, owner));
        }

        public void ChangeState(EnemyState newState)
        {
            if (currentState != null)
            {
                currentState.OnStateExit();
            }

            currentState = States[newState];
            currentState.OnStateEnter();
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }
    }
}
