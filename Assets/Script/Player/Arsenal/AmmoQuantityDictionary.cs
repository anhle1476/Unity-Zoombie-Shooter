using Script.Base.Collections;
using System;

namespace Script.Player.Arsenal
{
    [Serializable]
    public class AmmoQuantityDictionary : SerializableDictionary<AmmoType, int>
    {
        public AmmoQuantityDictionary()
        {
            foreach (AmmoType ammoType in Enum.GetValues(typeof(AmmoType)))
            {
                Add(ammoType, 0);
            }
        }

        /// <summary>
        /// Add ammo to the bag
        /// </summary>
        /// <param name="ammoType"></param>
        /// <param name="amount"></param>
        public void CollectAmmo(AmmoType ammoType, int amount)
        {
            this[ammoType] += amount;
        }

        public int UseAmmo(AmmoType ammoType, int amount)
        {
            int currentAmount = this[ammoType];

            if (currentAmount < amount)
            {
                this[ammoType] = 0;
                return currentAmount;
            }
            else
            {
                this[ammoType] -= amount;
                return amount;
            }
        }

        public int GetAmmoAmount(AmmoType ammoType)
        {
            return this[ammoType];
        }
    }
}
