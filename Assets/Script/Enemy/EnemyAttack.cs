using System;
using Script.Base.Fighting;
using UnityEngine;

namespace Script.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField]
        private int damage;
        private Transform _target;

        private void Start()
        {
            _target = GetComponent<EnemyAI>().Target;
        }

        private void ApplyDamage()
        {
            var dmg = new Damage
            {
                origin = transform.position,
                damageAmount = damage,
            };
            _target.SendMessage(nameof(IDamageable.TakeDamage), dmg, SendMessageOptions.DontRequireReceiver);
        }
    }
}