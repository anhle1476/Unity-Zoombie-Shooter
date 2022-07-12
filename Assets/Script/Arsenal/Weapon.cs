using Script.Base.Constants;
using Script.Base.Fighting;
using Script.Effects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Arsenal
{
    public class Weapon : MonoBehaviour
    {
        #region serialize fields

        [SerializeField]
        private Camera FPCamera;

        [SerializeField]
        private GameObject muzzleFlash;
        [SerializeField]
        private AutoDisableFX hitEffect;

        [SerializeField]
        [Range(0.1f, 10000f)]
        private float range = 100f;

        [SerializeField]
        [Range(1, 10000)]
        private int weaponDamage = 30;

        [SerializeField]
        [Range(0.01f, 120f)]
        [Tooltip("The cooldown between 2 shots")]
        private float firingCooldown = 0.25f;

        [SerializeField]
        private AmmoType ammoType = AmmoType.Riffle;

        [SerializeField]
        private int magazineSize = 30;

        [SerializeField]
        [Tooltip("The weapon inactivate period for reloading ammo")]
        private float reloadCooldown = 1f;

        #endregion

        #region private fields

        private float _lastShotTime;
        private float _lastReloadTime;

        private AutoDisableFXPool _hitEffectVFXPool;

        private AmmoBag _ammoBag;

        private List<ParticleSystem> _muzzleFlashVFXs = new List<ParticleSystem>();

        private int _remainingAmmo;

        #endregion

        #region getters

        public int MagazineSize => magazineSize;

        public int RemainingAmmo => _remainingAmmo;

        public AmmoType AmmoType => ammoType;

        #endregion

        #region Unity methods

        private void Start()
        {
            _remainingAmmo = magazineSize;

            if (!FPCamera)
            {
                FPCamera = Camera.main;
            }

            if (!_ammoBag)
            {
                _ammoBag = GetComponentInParent<AmmoBag>();
            }

            if (muzzleFlash)
            {
                _muzzleFlashVFXs = muzzleFlash.GetComponentsInChildren<ParticleSystem>().ToList();
            }

            _hitEffectVFXPool = new AutoDisableFXPool(hitEffect);
        }

        // Update is called once per frame
        void Update()
        {
            bool isReloadingTime = Time.time - _lastReloadTime <= reloadCooldown;
            if (isReloadingTime) return;

            if (Input.GetButton(InputConst.FIRE_1))
            {
                if (Time.time - _lastShotTime > firingCooldown)
                {
                    Shoot();
                }
            }

            if (Input.GetButton(InputConst.RELOAD) && _remainingAmmo < magazineSize)
            {
                ReloadAmmo();
            }
        }

        #endregion

        private void Shoot()
        {
            if (_remainingAmmo <= 0)
                return;

            _remainingAmmo--;

            PlayMuzzleFlash();

            _lastShotTime = Time.time;
            bool isHit = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hitInfo, range);
            if (!isHit) return;

            var dmg = new Damage
            {
                origin = FPCamera.transform.position,
                damageAmount = weaponDamage
            };
            hitInfo.collider.SendMessage(nameof(IDamageable.TakeDamage), value: dmg, SendMessageOptions.DontRequireReceiver);

            PlayHitEffect(hitInfo);

            if (_remainingAmmo == 0)
            {
                ReloadAmmo();
            }
        }

        private void PlayHitEffect(RaycastHit hit)
        {
            AutoDisableFX hitEffectObj = _hitEffectVFXPool.GetInactiveObjectFromPool();
            hitEffectObj.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            hitEffectObj.gameObject.SetActive(true);
        }

        private void PlayMuzzleFlash()
        {
            foreach (ParticleSystem particle in _muzzleFlashVFXs)
            {
                particle.Play();
            }
        }

        /// <summary>
        /// Perform reload action and update the last reload time if there are enough ammo for reload
        /// </summary>
        /// <returns></returns>
        private bool ReloadAmmo()
        {
            bool isReloadSuccess = _ammoBag.ReloadWeaponMagazine(this);

            if (isReloadSuccess)
            {
                _lastReloadTime = Time.time;
            }

            return isReloadSuccess;
        }

        /// <summary>
        /// To be called by the <see cref="AmmoBag"/> to reload the ammo for this weapon
        /// </summary>
        /// <param name="amount"></param>
        public void LoadNewAmmo(int amount)
        {
            _remainingAmmo += amount;
            if (_remainingAmmo > magazineSize)
            {
                Debug.LogError($"The loaded ammo {_remainingAmmo} is bigger than magazine size {magazineSize}. Reduce to magazine size");
                _remainingAmmo = magazineSize;
            }
        }
    }
}
