using UnityEngine;

namespace DPX.Weapons
{
    public class GunWeapon : WeaponView
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject fire;
        [SerializeField] private GameObject hitPoint;

        public override void Attack()
        {
            if (!CanAttack) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                targetPoint = hit.point;

                GameObject muzzle = Instantiate(fire, firePoint.position, firePoint.rotation, firePoint);
                Destroy(muzzle, 1f);

                GameObject impact = Instantiate(hitPoint, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);

                firePoint.LookAt(hit.point);
            }
            else
            {
                targetPoint = ray.GetPoint(100f);
                firePoint.LookAt(targetPoint);
            }

            cooldownTimer = weaponData.cooldown;
        }
    }
}