using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu {
    public class Menu : MonoBehaviour
    {
        private void Update()
        {
            if (!(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I)))
            {
                return;
            }
            SceneManager.UnloadSceneAsync("Menu");
            Time.timeScale = 1;
        }
    
    }
}
