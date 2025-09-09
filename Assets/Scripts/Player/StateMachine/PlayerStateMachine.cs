using DPX.Interfaces;
using System.Collections.Generic;

namespace DPX.Player.StateMachine
{
    public class PlayerStateMachine
    {
        private PlayerController Owner;

        private IPlayerState currentState;

        protected Dictionary<PlayerState, IPlayerState> States = new Dictionary<PlayerState, IPlayerState>();

        public PlayerStateMachine(PlayerController Owner)
        {
            this.Owner = Owner;
            CreateState();
            SetOwner();
        }

        private void CreateState()
        {
            States.Add(PlayerState.IDLE, new PlayerIdleState(this));
            States.Add(PlayerState.MOVE, new PlayerMoveState(this));
            States.Add(PlayerState.ROTATE, new PlayerRotateState(this));
            States.Add(PlayerState.ATTACK, new PlayerAttackState(this));
        }

        private void SetOwner()
        {
            foreach(IPlayerState state in States.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IPlayerState state)
        {
            currentState?.OnStateExit();
            currentState = state;
            currentState?.OnStateEnter();
        }

        public void ChangeState(PlayerState state) => ChangeState(States[state]);
    }
}
