using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Base.Utils
{
    public class GameManager : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void ReloadScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}