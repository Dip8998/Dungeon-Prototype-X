using DPX.GameHealth;
using DPX.Main;
using System.Collections;
using System.Collections.Generic;
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
        private HashSet<Health> alreadyHit = new HashSet<Health>();

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
                    EndAttack();

                if (slashObject)
                    slashObject.transform.Rotate(Vector3.up * weaponData.rotationSpeed * Time.deltaTime);

                PerformAttack(); 
            }
        }


        private void EndAttack()
        {
            isAttacking = false;
            alreadyHit.Clear(); 
            if (slashObject) slashObject.SetActive(false);
        }

        private void PerformAttack()
        {
            Vector3 origin = slashObject ? slashObject.transform.position : transform.position;

            Collider[] hits = Physics.OverlapSphere(origin, attackRange, enemyLayer);

            foreach (var hit in hits)
            {
                var health = hit.GetComponent<Health>();
                if (health != null && !alreadyHit.Contains(health))
                {
                    Vector3 dir = (hit.transform.position - origin).normalized;
                    health.TakeDamageWithKnockback(damage, dir, 10f);

                    alreadyHit.Add(health);

                    Vector3 hitPoint = hit.ClosestPoint(origin);
                    Vector3 hitNormal = (hitPoint - origin).normalized;

                    GameObject impact = GameService.Instance.VFXService.GetObject("HitPoint");
                    impact.transform.SetPositionAndRotation(hitPoint, Quaternion.LookRotation(hitNormal));
                    StartCoroutine(ReturnEffectToPoolAfterDelay(impact, "HitPoint", 2f));
                }
            }
        }


        private IEnumerator ReturnEffectToPoolAfterDelay(GameObject obj, string poolName, float delay)
        {
            yield return new WaitForSeconds(delay);
            GameService.Instance.VFXService.ReturnObject(poolName, obj);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
