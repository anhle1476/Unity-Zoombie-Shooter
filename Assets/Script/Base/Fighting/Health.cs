using UnityEngine;

namespace Script.Base.Fighting
{
    public abstract class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        protected int hitPoints = 100;

        public int HitPoints => hitPoints;

        private bool IsDead => hitPoints <= 0;

        public virtual void TakeDamage(Damage damage) 
        {
            if (IsDead) return;
            
            hitPoints -= damage.damageAmount;
            if (IsDead)
            {
                OnDeath();
            }
            
            PostTakeDamage();
        }

        protected virtual void PostTakeDamage()
        {
            
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}