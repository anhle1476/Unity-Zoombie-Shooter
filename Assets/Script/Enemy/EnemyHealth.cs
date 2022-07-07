using System;
using Script.Base.Fighting;
using UnityEngine;

namespace Script.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    public class EnemyHealth : Health
    {
        private EnemyAI _enemyAI;
        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
        }

        protected override void PostTakeDamage()
        {
            _enemyAI.IsProvoked = true;
        }
    }
}