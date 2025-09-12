using UnityEngine;

namespace DPX.GameHealth
{
    public interface IKnockbackReceiver
    {
        void ApplyKnockback(Vector3 direction, float force);
    }
}
