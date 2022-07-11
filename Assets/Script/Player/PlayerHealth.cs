using Script.Base.Fighting;
using Script.Base.Utils;
using UnityEngine;

namespace Script.Player
{
    public class PlayerHealth : Health
    {
        private DeathHandler _deathHandler;

        public DeathHandler DeathHandler { get; set; }

        private void Start()
        {
            _deathHandler = FindObjectOfType<DeathHandler>();
        }

        protected override void OnDeath()
        {
            Debug.Log("Game Over");
            _deathHandler.HandleDeath();
        }

        protected override void PostTakeDamage()
        {
            Debug.Log("Player remaining health: " + hitPoints);
        }
    }
}