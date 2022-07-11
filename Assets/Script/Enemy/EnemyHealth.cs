using Script.Base.Fighting;
using UnityEngine;

namespace Script.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    public class EnemyHealth : Health
    {
        private static readonly int Death = Animator.StringToHash("Death");

        private Animator _animator;
        private EnemyAI _enemyAI;

        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
            _animator = GetComponent<Animator>();
        }

        protected override void PostTakeDamage()
        {
            _enemyAI.IsProvoked = true;
        }

        protected override void OnDeath()
        {
            _animator.SetTrigger(Death);
            GetComponent<EnemyAI>().enabled = false;
        }
    }
}