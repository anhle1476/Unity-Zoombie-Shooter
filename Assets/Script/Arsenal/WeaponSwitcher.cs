using Script.Base.Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Arsenal
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField]
        private List<Weapon> weapons = new();

        [SerializeField]
        private float weaponSwitchSpeed = 10f;

        private int _currentWeaponIndex = 0;

        private float _accumulatedSwitchValue = 0;

        public Weapon CurrentWeapon => weapons.ElementAt(_currentWeaponIndex);

        void Start()
        {
            if (!weapons.Any())
            {
                weapons = GetComponentsInChildren<Weapon>().ToList();
            }

            ActivateWeapon();
        }

        void Update()
        {
            _accumulatedSwitchValue += Input.GetAxis(InputConst.MOUSE_SCROLLWHEEL) * weaponSwitchSpeed;


        }

        private void FixedUpdate()
        {
            SwitchWeapon();
        }

        private void SwitchWeapon()
        {
            int roundedValue = Mathf.FloorToInt(_accumulatedSwitchValue);
            int numberOfWeapons = weapons.Count();

            // convert to next weapon index
            int nextIndex = (roundedValue % numberOfWeapons);
            if (nextIndex < 0)
            {
                nextIndex += numberOfWeapons;
            }

            // start switch weapon if the index is different
            if (nextIndex != _currentWeaponIndex)
            {
                _currentWeaponIndex = nextIndex;
                ActivateWeapon();
            }
        }

        void ActivateWeapon()
        {
            for (int index = 0; index < weapons.Count; index++)
            {
                Weapon currentWeapon = weapons.ElementAt(index);
                currentWeapon.gameObject.SetActive(index == _currentWeaponIndex);
            }
        }
    }
}