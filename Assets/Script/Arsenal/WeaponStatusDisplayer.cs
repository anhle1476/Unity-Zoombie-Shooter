using Script.Arsenal;
using TMPro;
using UnityEngine;

namespace Assets.Script.Arsenal
{
    [RequireComponent(typeof(TMP_Text))]
    public class WeaponStatusDisplayer : MonoBehaviour
    {
        private TMP_Text _displayText;
        private WeaponSwitcher _weaponSwitcher;

        // Use this for initialization
        void Start()
        {
            _displayText = GetComponent<TMP_Text>();
            _weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Weapon weapon = _weaponSwitcher.CurrentWeapon;
            _displayText.text = $"{weapon.RemainingAmmo} / {weapon.MagazineSize}";
        }
    }
}