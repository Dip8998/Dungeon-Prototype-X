namespace DPX.Interfaces
{
    public interface IEnemyState
    {
        public void OnStateEnter();
        public void Update();
        public void OnStateExit();
    }
}
