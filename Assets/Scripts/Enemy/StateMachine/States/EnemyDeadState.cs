using DPX.Enemy.StateMachine;
using DPX.Enemy;
using UnityEngine;
using DPX.Interfaces;

namespace DPX.Enemies.StateMachine
{
    public class EnemyDeadState : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        private EnemyController owner;

        public EnemyDeadState(EnemyStateMachine stateMachine, EnemyController owner)
        {
            this.stateMachine = stateMachine;
            this.owner = owner;
        }

        public void OnStateEnter()
        {
            GameObject.Destroy(owner.EnemyView.gameObject);
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}
