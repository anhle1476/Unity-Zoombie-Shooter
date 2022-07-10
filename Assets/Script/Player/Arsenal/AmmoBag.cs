using UnityEngine;

namespace Script.Player.Arsenal
{
    public class AmmoBag : MonoBehaviour
    {
        [SerializeField]
        private AmmoQuantityDictionary quantityDict = new AmmoQuantityDictionary();

        [SerializeField]
        [Range(0, 1000)]
        private int defaultAmmoAmount = 20;


        // Use this for initialization
        void Start()
        {
            foreach (var ammoType in quantityDict.Keys)
            {
                quantityDict.CollectAmmo(ammoType, defaultAmmoAmount);
            }
        }

        public bool ReloadWeaponMagazine(Weapon weapon)
        {
            int requestedAmount = weapon.MagazineSize - weapon.RemainingAmmo;
            int providedAmmo = quantityDict.UseAmmo(weapon.AmmoType, requestedAmount);

            if (providedAmmo == 0)
                return false;

            weapon.LoadNewAmmo(providedAmmo);
            return true;
        }
    }
}