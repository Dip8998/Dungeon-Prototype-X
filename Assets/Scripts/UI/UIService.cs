using DPX.ScriptableObjects;
using UnityEngine;

namespace DPX.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private GameplayUIView gameplayUIView;

        public void SetPlayerWeaponIcon(WeaponType type) => gameplayUIView.UpdateWeaponUIIcone(type);

        public void SetImageActive(bool active) => gameplayUIView.SetImage(active); 
    }
}
