using DPX.Interfaces;
using DPX.ScriptableObjects;
using UnityEngine;

namespace DPX.Weapons
{
    public abstract class PlayerWeaponView : MonoBehaviour, IWeapon
    {
        [SerializeField] protected WeaponSO weaponData;

        protected float cooldownTimer;

        public bool CanAttack => cooldownTimer <= 0f;

        public virtual void UpdateWeapon()
        {
            if (cooldownTimer > 0f)
                cooldownTimer -= Time.deltaTime;
        }

        public virtual void OnEquip()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnUnequip()
        {
            gameObject.SetActive(false);
        }

        public abstract void Attack();
    }
}