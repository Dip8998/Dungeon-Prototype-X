using DPX.Interfaces;

namespace DPX.Player.StateMachine
{
    public class RotateState : IState
    {
        public PlayerController Owner { get; set; }
        private PlayerStateMachine stateMachine;

        public RotateState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() { }

        public void Update()
        {
            if(Owner.Inputs.MoveInput.sqrMagnitude > 0.1f)
            {
                stateMachine.ChangeState(PlayerState.MOVE);
            }
            else if (Owner.Inputs.AttackInput)
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
