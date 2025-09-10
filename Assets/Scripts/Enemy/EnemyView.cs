using DPX.GameHealth;
using UnityEngine;
using UnityEngine.UI;

namespace DPX.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController controller;
        private Health enemyHealth;

        [Header("UI")]
        [SerializeField] private Image healthFillImage;

        public void SetController(EnemyController playerController)
        {
            this.controller = playerController;
            enemyHealth = GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.OnDeath += Die;
                enemyHealth.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
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
