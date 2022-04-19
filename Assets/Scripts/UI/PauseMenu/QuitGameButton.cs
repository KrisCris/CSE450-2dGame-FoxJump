using UnityEngine;
using UnityEngine.UI;

namespace UI.PauseMenu {
    public class QuitGameButton : MonoBehaviour
    {
        public Button quitGameButton;
        void Start()
        {
            quitGameButton.onClick.AddListener(() =>
            {
                SceneController.Instance.ReturnMainPage();
            });
        }
    }
}
