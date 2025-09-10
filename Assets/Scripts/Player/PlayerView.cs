using DPX.GameHealth;
using UnityEngine;
using UnityEngine.UI;

namespace DPX.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController controller;
        private Health playerHealth;

        [Header("UI")]
        [SerializeField] private Image healthFillImage; 

        public void SetController(PlayerController playerController)
        {
            this.controller = playerController;
            playerHealth = GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.OnDeath += Die;
                playerHealth.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth); 
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
