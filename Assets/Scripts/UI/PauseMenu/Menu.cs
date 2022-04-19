using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu {
    public class Menu : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }
            SceneManager.UnloadSceneAsync("Menu");
            Time.timeScale = 1;
        }
    
    }
}
