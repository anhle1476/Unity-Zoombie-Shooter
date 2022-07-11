using Script.Base.Fighting;
using UnityEngine;

namespace Script.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField]
        private int damage;
        private EnemyAI _enemyAI;

        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
        }

        /// <summary>
        /// To be called in the enemy attack animation
        /// </summary>
        private void ApplyDamage()
        {
            var dmg = new Damage
            {
                origin = transform.position,
                damageAmount = damage,
            };
            _enemyAI.Target.SendMessage(nameof(IDamageable.TakeDamage), dmg, SendMessageOptions.DontRequireReceiver);
        }
    }
}