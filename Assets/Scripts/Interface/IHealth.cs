using UnityEngine;

namespace DPX.Interfaces
{
	public interface IHealth 
	{
        void TakeDamage(float amount);
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsDead { get; }
    }
}
