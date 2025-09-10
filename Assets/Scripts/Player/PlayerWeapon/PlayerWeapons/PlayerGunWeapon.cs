using DPX.GameHealth;
using DPX.Main;
using System.Collections;
using UnityEngine;

namespace DPX.Weapons
{
    public class PlayerGunWeapon : PlayerWeaponView
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private LayerMask attackLayer;

        public override void Attack()
        {
            if (!CanAttack) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            GameObject muzzle = GameService.Instance.VFXService.GetObject("MuzzleFire");
            muzzle.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            StartCoroutine(ReturnEffectToPoolAfterDelay(muzzle, "MuzzleFire", 1f));

            if (Physics.Raycast(ray, out hit, 100f, attackLayer))
            {
                Health targetHealth = hit.collider.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(10f);
                }

                GameObject impact = GameService.Instance.VFXService.GetObject("HitPoint");
                impact.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
                StartCoroutine(ReturnEffectToPoolAfterDelay(impact, "HitPoint", 2f));

                firePoint.LookAt(hit.point);
            }
            else
            {
                Vector3 targetPoint = ray.GetPoint(100f);
                firePoint.LookAt(targetPoint);
            }

            cooldownTimer = weaponData.cooldown;
        }

        private IEnumerator ReturnEffectToPoolAfterDelay(GameObject obj, string poolName, float delay)
        {
            yield return new WaitForSeconds(delay);
            GameService.Instance.VFXService.ReturnObject(poolName, obj);
        }
    }
}