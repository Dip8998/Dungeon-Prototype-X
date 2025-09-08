using DPX.ScriptableObjects;
using DPX.Weapons;
using UnityEngine;

namespace DPX.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerData;
        [SerializeField] private WeaponView[] startingWeapons;

        private PlayerController controller;

        private void Awake()
        {
            controller = new PlayerController(this, playerData);
            foreach (var w in startingWeapons)
                if (w) controller.AddWeapon(w);
        }

        private void Start() => controller.StartPlayer();

        private void Update() => controller.UpdatePlayer();

        public void CollectWeapon(WeaponView w) => controller.AddWeapon(w);

    }
}