using Script.Base.Fighting;
using UnityEngine;

namespace Script.Player.Fighting
{
    public class PlayerHealth : Health
    {
        protected override void OnDeath()
        {
            Debug.Log("Game Over");
        }

        protected override void PostTakeDamage()
        {
            Debug.Log("Player remaining health: " + hitPoints);
        }
    }
}