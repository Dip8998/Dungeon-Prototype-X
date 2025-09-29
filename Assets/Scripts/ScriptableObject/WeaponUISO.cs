using System.Collections.Generic;
using UnityEngine;

namespace DPX.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponUI_SO", menuName = "ScriptableObjects/WeaponUISO")]
    public class WeaponUISO : ScriptableObject
    {
        public List<WeaponIconData> WeaponIcons;

        public Sprite GetIcon(WeaponType type)
        {
            foreach(WeaponIconData icon in WeaponIcons)
            {
                if(icon.WeaponType == type)
                {
                    return icon.WeaponIcon;
                }
            }

            return null;
        }
    }
}
