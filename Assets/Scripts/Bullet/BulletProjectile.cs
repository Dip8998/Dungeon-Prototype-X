using UnityEngine;

namespace DPX.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletProjectile : MonoBehaviour
    {
        private float damage;
        private float life;

        public void Initialize(float damage, float lifeSeconds)
        {
            this.damage = damage;
            life = lifeSeconds;
            Destroy(gameObject, life);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}
