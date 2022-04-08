using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class RestartButton : MonoBehaviour {
        // public Button button;
        void Start() {
            // GetComponent<Button>().onClick.AddListener(() =>
            // {
            //     SceneManager.UnloadSceneAsync("Scene_0");
            //     SceneManager.UnloadSceneAsync("Player");
            //     SceneManager.UnloadSceneAsync("Info");
            //     // SceneManager.LoadScene("GamePlay");
            //     SceneManager.LoadScene("Scene_0", LoadSceneMode.Additive);
            //     SceneManager.LoadScene("Player", LoadSceneMode.Additive);
            //     Time.timeScale = 1;
            // });
            if (SceneController.Instance) {
                GetComponent<Button>().onClick.AddListener(SceneController.Instance.ReloadCurrentScene);
            }
        }
    }
}