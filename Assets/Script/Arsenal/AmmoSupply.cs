using UnityEngine;

namespace Script.Arsenal
{
    public class AmmoSupply : MonoBehaviour
    {
        [SerializeField]
        private AmmoType ammoType = AmmoType.Riffle;
        [SerializeField]
        [Range(1, 100)]
        private int amount = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") == false)
                return;

            AmmoBag ammoBag = other.GetComponentInChildren<AmmoBag>();

            if (ammoBag)
            {
                ammoBag.AmmoQuantity.CollectAmmo(ammoType, amount);

                Destroy(gameObject);
            }
        }
    }
}