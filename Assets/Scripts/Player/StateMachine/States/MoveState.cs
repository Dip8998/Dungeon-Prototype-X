using DPX.Interfaces;

namespace DPX.Player.StateMachine
{
    public class MoveState : IState
    {
        public PlayerController Owner {  get; set; }
        private PlayerStateMachine stateMachine;

        public MoveState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() { }

        public void Update()
        {
            if(Owner.Inputs.MoveInput.sqrMagnitude < 0.1f)
            {
                stateMachine.ChangeState(PlayerState.IDLE);
            }
            else if (Owner.Inputs.AttackInput)
            {
                stateMachine.ChangeState(PlayerState.ATTACK);
            }

            Owner.HandleMovement();
        }

        public void OnStateExit() { }
    }
}
