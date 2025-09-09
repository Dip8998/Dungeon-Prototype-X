using DPX.ScriptableObjects;
using DPX.Weapons;
using UnityEngine;

namespace DPX.Player
{
    public class PlayerView : MonoBehaviour
    {
        //[SerializeField] private PlayerSO playerData;
        //[SerializeField] private WeaponView[] startingWeapons;

        private PlayerController controller;

        public void SetController(PlayerController playerController)
        {
            this.controller = playerController;

            //foreach (var w in startingWeapons)
            //{
            //    if (w) controller.AddWeapon(w);
            //}
        }
    }
}