using DPX.GameHealth;
using DPX.GameStamina;
using UnityEngine;
using UnityEngine.UI;

namespace DPX.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController controller;
        private Health playerHealth;
        private Stamina playerStamina;

        [Header("UI")]
        [SerializeField] private Image healthFillImage;
        [SerializeField] private Image staminaFillImage;

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

            playerStamina = GetComponent<Stamina>();
            if (playerStamina != null)
            {
                playerStamina.OnStaminaChanged += UpdateStaminaBar;
                UpdateStaminaBar(playerStamina.CurrentStamina, playerStamina.MaxStamina);
            }
        }

        private void UpdateHealthBar(float current, float max)
        {
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = current / max; 
            }
        }

        private void UpdateStaminaBar(float current, float max)
        {
            if (staminaFillImage != null)
                staminaFillImage.fillAmount = current / max;
        }

        private void Die()
        {
            Destroy(this.gameObject);
        }
    }
}
