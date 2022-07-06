using System;
using System.Collections.Generic;
using System.Linq;
using Script.Base.Constants;
using Script.Base.Fighting;
using Script.Effects;
using UnityEngine;

namespace Script
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] 
        private Camera FPCamera;

        [SerializeField] private GameObject muzzleFlash;
        [SerializeField] private AutoDisableFX hitEffect;
        
        private List<ParticleSystem> _muzzleFlashVFXs = new List<ParticleSystem>();

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

        private float _lastShotTime;

        private AutoDisableFXPool _hitEffectVFXPool;
        
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

        private void Shoot()
        {
            _lastShotTime = Time.time;
            bool isHit = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hitInfo, range);
            if (isHit)
            {
                var dmg = new Damage
                {
                    origin = FPCamera.transform.position,
                    damageAmount = weaponDamage
                };
                hitInfo.collider.SendMessage(nameof(IDamageable.TakeDamage), value: dmg, SendMessageOptions.DontRequireReceiver);

                PlayHitEffect(hitInfo.point);
            }
        }

        private void PlayHitEffect(Vector3 hitPosition)
        {
            AutoDisableFX hitEffectObj = _hitEffectVFXPool.GetInactiveObjectFromPool();
            hitEffectObj.transform.position = hitPosition;
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
