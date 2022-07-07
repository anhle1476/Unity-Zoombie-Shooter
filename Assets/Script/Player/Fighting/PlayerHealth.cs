using System;
using Script.Base.Fighting;
using Script.Base.Utils;
using UnityEngine;

namespace Script.Player.Fighting
{
    public class PlayerHealth : Health
    {
        private DeathHandler _deathHandler;

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