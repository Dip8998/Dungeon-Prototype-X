using DPX.ScriptableObjects;
using DPX.Weapons;

namespace DPX.Player
{
    public class PlayerService
    {
        private PlayerController playerController;

        public PlayerService(PlayerSO playerSO, PlayerView playerView)
        {
            playerController = new PlayerController(playerView, playerSO);
            playerView.SetController(playerController);
        }

        public void Init()
        {
            playerController.StartPlayer();
        }

        public void Update()
        {
            playerController.UpdatePlayer();
        }

        public void AddWeaponToPlayer(PlayerWeaponView weapon) => playerController.AddWeapon(weapon);

        public PlayerController GetPlayerController() => playerController;
    }
}
