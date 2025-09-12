using DPX.GameHealth;
using UnityEngine;
using UnityEngine.UI;

namespace DPX.Enemy
{
    public class EnemyView : MonoBehaviour, IKnockbackReceiver
    {
        private EnemyController controller;
        private Health enemyHealth;
        public CharacterController Controller { get; private set; }
        private Vector3 knockbackVelocity;
        private float knockbackTimer;

        [Header("UI")]
        [SerializeField] private Image healthFillImage;
        [SerializeField] private float knockbackDuration = 0.2f;

        public void SetController(EnemyController playerController)
        {
            this.controller = playerController;
            Controller = GetComponent<CharacterController>();
            enemyHealth = GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.OnDeath += Die;
                enemyHealth.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
            }   
        }

        private void Update()
        {
            HandleKnockback();
        }

        public void ApplyKnockback(Vector3 direction, float force)
        {
            knockbackVelocity = direction.normalized * force;
            knockbackTimer = knockbackDuration;
        }

        private void HandleKnockback()
        {
            if (knockbackTimer > 0f)
            {
                Controller.Move(knockbackVelocity * Time.deltaTime);
                knockbackTimer -= Time.deltaTime;
            }
        }

        private void UpdateHealthBar(float current, float max)
        {
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = current / max;
            }
        }

        private void Die()
        {
            Destroy(this.gameObject);
        }
    }
}
