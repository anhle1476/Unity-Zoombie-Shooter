using System;
using UnityEngine;

namespace Script
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] 
        private Camera FPCamera;
        [SerializeField]
        private ParticleSystem muzzleFlashVFX;

        [SerializeField] 
        [Range(0.1f, 10000f)]
        private float range = 100f;
        [SerializeField]
        [Range(1, 10000)]
        private int weaponDamage = 30;
        
        private void Start()
        {
            if (!FPCamera)
            {
               FPCamera = GetComponentInParent<Camera>();
            }

            if (muzzleFlashVFX)
            {
                muzzleFlashVFX = GetComponentInChildren<ParticleSystem>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(InputConst.FIRE_1))
            {
                Shoot();
                PlayMuzzleFlash();
            }
        }

        private void Shoot()
        {
            bool isHit = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hitInfo, range);
            if (isHit)
            {
                var dmg = new Damage
                {
                    origin = FPCamera.transform.position,
                    damageAmount = weaponDamage
                };
                hitInfo.collider.SendMessage(nameof(IDamageable.TakeDamage), value: dmg, SendMessageOptions.DontRequireReceiver);
            }
        }
        
        private void PlayMuzzleFlash()
        {
            if (muzzleFlashVFX)
            {
                muzzleFlashVFX.Play();
            }
        }
    }
}
