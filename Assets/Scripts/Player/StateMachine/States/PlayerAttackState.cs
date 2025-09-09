using DPX.Interfaces;

namespace DPX.Player.StateMachine
{
    public class PlayerAttackState : IPlayerState
    {
        public PlayerController Owner { get; set; }
        private PlayerStateMachine stateMachine;

        public PlayerAttackState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            Owner.HandleAttack();
        }

        public void Update()
        {
            if(Owner.Inputs.MoveInput.sqrMagnitude > 0.1f)
            {
                stateMachine.ChangeState(PlayerState.MOVE);
            }
            else
            {
                stateMachine.ChangeState(PlayerState.IDLE);
            }
        }

        public void OnStateExit() { }
    }
}
