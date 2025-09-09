using UnityEngine;

namespace DPX.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy_SO", menuName = "ScriptableObject/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public float health = 100f;
        public float moveSpeed = 3f;
        public float rotationSpeed = 5f;
        public float chaseDistance = 10f;
        public float attackRange = 2f;
        public float attackDamage = 10f;
        public float attackCooldown = 2f;
    }
}
