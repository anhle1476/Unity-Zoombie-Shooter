using System.Collections.Generic;
using System.Linq;
using Script.Base.Constants;
using Script.Base.Fighting;
using Script.Effects;
using UnityEngine;

namespace Script.Player.Arsenal
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
        private AmmoType ammoType = AmmoType.Bullet;

        #endregion

        #region private fields

        private float _lastShotTime;

        private AutoDisableFXPool _hitEffectVFXPool;
        
        private List<ParticleSystem> _muzzleFlashVFXs = new List<ParticleSystem>();

        #endregion

        #region Unity methods

        private void Start()
        {
            if (!FPCamera)
            {
               FPCamera = GetComponentInParent<Camera>();
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
            if (Input.GetButton(InputConst.FIRE_1))
            {
                if (Time.time - _lastShotTime > firingCooldown)
                {
                    Shoot();
                    PlayMuzzleFlash();
                }
            }
        }

        #endregion

        private void Shoot()
        {
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
        }

        private void PlayHitEffect(RaycastHit hit)
        {
            AutoDisableFX hitEffectObj = _hitEffectVFXPool.GetInactiveObjectFromPool();
            hitEffectObj.transform.position = hit.point;
            hitEffectObj.transform.rotation = Quaternion.LookRotation(hit.normal);
            hitEffectObj.gameObject.SetActive(true);
        }
        
        private void PlayMuzzleFlash()
        {
            foreach (ParticleSystem particle in _muzzleFlashVFXs)
            {
                particle.Play();
            }
        }
    }
}
