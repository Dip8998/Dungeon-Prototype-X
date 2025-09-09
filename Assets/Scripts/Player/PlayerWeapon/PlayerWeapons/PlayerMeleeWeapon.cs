using UnityEngine;
using System.Collections;

namespace DPX.Weapons
{
    public class PlayerMeleeWeapon : PlayerWeaponView
    {
        [SerializeField] private GameObject slashObject;
        private bool isAttacking;
        private float attackTimer;

        private void Awake()
        {
            if (slashObject) slashObject.SetActive(false);
        }

        public override void OnEquip()
        {
            base.OnEquip();
            if (slashObject) slashObject.SetActive(false);
        }

        public override void Attack()
        {
            if (!CanAttack || isAttacking) return;

            isAttacking = true;
            attackTimer = weaponData.attackDuration;
            cooldownTimer = weaponData.cooldown;

            if (slashObject) slashObject.SetActive(true);
        }

        public override void UpdateWeapon()
        {
            base.UpdateWeapon();

            if (isAttacking)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    EndAttack();
                }

                if (slashObject)
                    slashObject.transform.Rotate(Vector3.up * weaponData.rotationSpeed * Time.deltaTime);
            }
        }

        private void EndAttack()
        {
            isAttacking = false;
            if (slashObject) slashObject.SetActive(false);
        }
    }
}