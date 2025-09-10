using DPX.Interfaces;
using DPX.Main;
using System;
using UnityEngine;

namespace DPX.GameHealth
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHealth = 100f;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0f;

        public event Action<float, float> OnHealthChanged;
        public event Action OnDeath;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if(IsDead) return;

            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (IsDead) Die();
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
