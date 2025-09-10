using DPX.GameHealth;
using UnityEngine;

namespace DPX.Weapons
{
    public class PlayerMeleeWeapon : PlayerWeaponView
    {
        [Header("Melee Settings")]
        [SerializeField] private GameObject slashObject;
        [SerializeField] private float damage = 20f;
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private LayerMask enemyLayer;

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

            PerformAttack();
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

        private void PerformAttack()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (var hit in hits)
            {
                var health = hit.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
