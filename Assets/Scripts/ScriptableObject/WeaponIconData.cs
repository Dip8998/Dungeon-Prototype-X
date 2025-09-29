using UnityEngine;

namespace DPX.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponIconData_SO", menuName = "ScriptableObjects/WeaponIconData_SO")]
    public class WeaponIconData : ScriptableObject
    {
        public WeaponType WeaponType;
        public Sprite WeaponIcon;
    }
}