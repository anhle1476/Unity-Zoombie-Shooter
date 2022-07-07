using System;
using UnityEngine;

namespace Script.Base.Utils
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;

        private void Start()
        {
            gameOverCanvas.gameObject.SetActive(false);
        }

        public void HandleDeath()
        {
            Time.timeScale = 0;
            
            gameOverCanvas.gameObject.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
