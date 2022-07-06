using UnityEngine;

namespace Script
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        protected int hitPoints = 100;

        public int HitPoints => hitPoints;

        private bool IsDead => hitPoints <= 0;

        public void TakeDamage(Damage damage)
        {
            hitPoints -= damage.damageAmount;
            if (IsDead)
            {
                OnDeath();
            }
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}