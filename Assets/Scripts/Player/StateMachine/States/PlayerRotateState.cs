using DPX.Interfaces;

namespace DPX.Player.StateMachine
{
    public class PlayerRotateState : IPlayerState
    {
        public PlayerController Owner { get; set; }
        private PlayerStateMachine stateMachine;

        public PlayerRotateState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() { }

        public void Update()
        {
            if(Owner.Inputs.MoveInput.sqrMagnitude > 0.1f)
            {
                stateMachine.ChangeState(PlayerState.MOVE);
            }
            else if (Owner.Inputs.FireInput || Owner.Inputs.MeleeInput)
            {
                stateMachine.ChangeState(PlayerState.ATTACK);
            }
            else if (!Owner.RotateTowardsMouse())
            {
                stateMachine.ChangeState(PlayerState.ROTATE);
            }
        }

        public void OnStateExit() { }
    }
}
