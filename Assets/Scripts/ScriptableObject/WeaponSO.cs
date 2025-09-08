using UnityEngine;

namespace DPX.ScriptableObjects
{
    public enum WeaponType { Melee, Ranged }

    [CreateAssetMenu(fileName = "NewWeapon_SO", menuName = "ScriptableObjects/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        [Header("Presentation")]
        public string weaponName = "Weapon";
        public Sprite icon;

        [Header("Shared")]
        public WeaponType type;
        public float cooldown = 0.3f;

        [Header("Melee")]
        public float attackDuration = 0.25f;
        public float rotationSpeed = 600f;
        public float meleeDamage = 10f;
        public float meleeHitRadius = 1.5f;
        public LayerMask meleeHitMask;
    }
}