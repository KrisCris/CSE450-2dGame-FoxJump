using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class RestartButton : MonoBehaviour {
        void Start() {
            if (SceneController.Instance) {
                GetComponent<Button>().onClick.AddListener(SceneController.Instance.ReloadCurrentScene);
            }
        }
    }
}