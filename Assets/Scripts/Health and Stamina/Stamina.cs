using System;
using UnityEngine;

namespace DPX.GameStamina
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float regenRate = 15f;
        [SerializeField] private float sprintDrainRate = 20f;

        public float CurrentStamina { get; private set; }
        public float MaxStamina => maxStamina;

        public bool IsExhausted => CurrentStamina <= 0f;

        public event Action<float, float> OnStaminaChanged;

        private void Awake()
        {
            CurrentStamina = maxStamina;
        }

        private void Update()
        {
            if (CurrentStamina < maxStamina)
            {
                ModifyStamina(regenRate * Time.deltaTime);
            }
        }

        public void DrainForSprint()
        {
            if (IsExhausted) return;

            ModifyStamina(-sprintDrainRate * Time.deltaTime);
        }

        private void ModifyStamina(float delta)
        {
            CurrentStamina = Mathf.Clamp(CurrentStamina + delta, 0f, maxStamina);
            OnStaminaChanged?.Invoke(CurrentStamina, maxStamina);
        }
    }
}
