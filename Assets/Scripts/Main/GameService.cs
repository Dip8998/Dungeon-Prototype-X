using UnityEngine;
using DPX.ScriptableObjects;
using DPX.Player;
using DPX.Utilities;
using System.Collections.Generic;
using DPX.Enemy;
using DPX.UI;

namespace DPX.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public EnemyService EnemyService { get; private set; }
        public VFXService VFXService { get; private set; }

        [SerializeField] private UIService uIService;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerSO;

        [Header("Enemy Setup")]
        [SerializeField] private List<EnemyView> enemyViews; 
        [SerializeField] private EnemySO enemySO;
        [SerializeField] private List<Transform> patrolPoints;

        [Header("VFX")]
        [SerializeField] private GameObject firePrefab;
        [SerializeField] private GameObject hitPointPrefab;

        public UIService UIService => uIService;

        protected override void Awake()
        {
            PlayerService = new PlayerService(playerSO, playerView);
            EnemyService = new EnemyService();
            VFXService = new VFXService(firePrefab, hitPointPrefab, this.transform);

            foreach (var enemy in enemyViews)
            {
                EnemyService.RegisterEnemy(enemy, enemySO);
            }
        }

        private void Start()
        {
            if (playerView != null)
            {
                PlayerService.Init();
            }

            EnemyService.InitAll(playerView.transform, patrolPoints);
        }

        private void Update()
        {
            if (playerView != null)
            {
                PlayerService.Update();
            }

            EnemyService.UpdateAll();
        }
    }
}
