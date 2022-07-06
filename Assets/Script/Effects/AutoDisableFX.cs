using System.Collections;
using UnityEngine;

namespace Script.Effects
{
    public class AutoDisableFX : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Countdown from the enable time until it's automatically disable")]
        private float lifeTime = 0.5f;

        private void OnEnable()
        {
            StartCoroutine(StartDisableCountdown(lifeTime));
        }

        private IEnumerator StartDisableCountdown(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }
    }
}