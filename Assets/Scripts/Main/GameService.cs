using UnityEngine;
using DPX.ScriptableObjects;
using DPX.Player;
using DPX.Utilities;

namespace DPX.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }


        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerSO;

        protected override void Awake()
        {
            PlayerService = new PlayerService(playerSO, playerView);
        }

        private void Start()
        {
            PlayerService.Init();
        }

        private void Update()
        {
            PlayerService.Update();
        }
    }
}