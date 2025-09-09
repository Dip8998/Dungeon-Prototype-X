using DPX.Player;

namespace DPX.Interfaces
{
    public interface IPlayerState
    {
        public PlayerController Owner { get; set; }
        public void OnStateEnter();
        public void Update();
        public void OnStateExit();
    }
}