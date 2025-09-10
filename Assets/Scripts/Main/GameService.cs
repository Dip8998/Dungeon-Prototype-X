using UnityEngine;
using DPX.ScriptableObjects;
using DPX.Player;
using DPX.Utilities;
using System.Collections.Generic;
using DPX.Enemy;

namespace DPX.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public EnemyService EnemyService { get; private set; }
        public VFXService VFXService { get; private set; }

        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private EnemyView enemyView;
        [SerializeField] private EnemySO enemySO;
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private GameObject firePrefab;
        [SerializeField] private GameObject hitPointPrefab;

        protected override void Awake()
        {
            PlayerService = new PlayerService(playerSO, playerView);
            EnemyService = new EnemyService(enemyView, enemySO);
            VFXService = new VFXService(firePrefab, hitPointPrefab, this.transform);
        }

        private void Start()
        {
            if(playerView != null)
            {
                PlayerService.Init();
            }

            if (enemyView != null)
            {
                EnemyService.Init(playerView.transform, patrolPoints);
            }
        }

        private void Update()
        {
            if (playerView != null)
            {
                PlayerService.Update();
            }

            if (enemyView != null)
            {
                EnemyService.Update();
            }
        }
    }
}