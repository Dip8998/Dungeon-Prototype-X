using DPX.Interfaces;
using System.Collections.Generic;

namespace DPX.Player.StateMachine
{
    public class PlayerStateMachine
    {
        private PlayerController Owner;

        private IState currentState;

        protected Dictionary<PlayerState, IState> States = new Dictionary<PlayerState, IState>();

        public PlayerStateMachine(PlayerController Owner)
        {
            this.Owner = Owner;
            CreateState();
            SetOwner();
        }

        private void CreateState()
        {
            States.Add(PlayerState.IDLE, new IdleState(this));
            States.Add(PlayerState.MOVE, new MoveState(this));
            States.Add(PlayerState.ROTATE, new RotateState(this));
            States.Add(PlayerState.ATTACK, new AttackState(this));
        }

        private void SetOwner()
        {
            foreach(IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState state)
        {
            currentState?.OnStateExit();
            currentState = state;
            currentState?.OnStateEnter();
        }

        public void ChangeState(PlayerState state) => ChangeState(States[state]);
    }
}
