using UnityEngine;

namespace DPX.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayer_SO", menuName = "ScriptableObjects/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        public float MoveSpeed;
        public float SmoothRotation;
        public float Gravity;
        public float GravityMultiplyer;
    }
}
