using DPX.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace DPX.UI
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] private Image weaponSprite;
        [SerializeField] private WeaponUISO weaponUISO;

        public void UpdateWeaponUIIcone(WeaponType weaponType)
        {
            Sprite newIcon = weaponUISO.GetIcon(weaponType);

            weaponSprite.sprite = newIcon;
        }

        public void SetImage(bool active)
        {
            weaponSprite.gameObject.SetActive(active);
        }
    }
}
