using UnityEngine;

namespace UI.MainMenu {
    public class MenuController: MonoBehaviour {
        public GameObject newGameButton;
        public GameObject continueButton;
        public GameObject selectLevelButton;

        private void Start() {
            if (GameController.Instance.selectableLevel.Count == 0) {
                continueButton.SetActive(false);
                
            } 
            // disable this btn
            selectLevelButton.SetActive(false);
            Debug.Log(PlayerPrefs.GetString("GameData"));
        }

        public void NewGame() {
            GameController.Instance.NewGame();
            SceneController.Instance.LoadGame("Scene_0");
        }

        public void ContinueGame() {
            SceneController.Instance.LoadGame(GameController.Instance.currentLevel);
        }

        public void SelectLevel() {
            foreach (var level in GameController.Instance.selectableLevel) {
                Debug.Log("Selectable: " + level);
            }
        }
    }
}