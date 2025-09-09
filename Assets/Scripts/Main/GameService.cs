using UnityEngine;
using DPX.ScriptableObjects;
using DPX.Player;
using DPX.Utilities;
using System.Collections.Generic;

namespace DPX.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public VFXService VFXService { get; private set; }

        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private GameObject firePrefab;
        [SerializeField] private GameObject hitPointPrefab;

        protected override void Awake()
        {
            PlayerService = new PlayerService(playerSO, playerView);
            VFXService = new VFXService(firePrefab, hitPointPrefab, this.transform);
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