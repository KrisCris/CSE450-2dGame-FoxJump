using System;
using Entity.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Instance;
    
    private string[] _gameSceneNames = new[] {
        "Scene_0"
    };
    
    private void Awake() {
        Instance = this;
    }
    private void Start()
    {
        if (!FindObjectOfType<SceneData>()) {
            SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
        }
    }

    public void LoadGame(string scene) {
        SceneManager.UnloadSceneAsync("MainUI");
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        GameController.Instance.SetCurrentScene(scene);
    }

    public void SwitchMap(String scene) {
        SceneManager.UnloadSceneAsync(GameController.Instance.currentLevel);
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        // GameController.Instance.SetLevelClear(SceneManager.GetActiveScene().name);
        GameController.Instance.SetLevelClear();
        GameController.Instance.SetCurrentScene(scene);
    }

    public void ReloadCurrentScene() {
        UnloadAll();
        GameController.Instance.Reload();
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        SceneManager.LoadScene(GameController.Instance.GetCurrentLevel(), LoadSceneMode.Additive);
    }

    public void ReturnMainPage() {
        UnloadAll();
        SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
    }

    public void UnloadAll() {
        for (int i = 1; i < SceneManager.sceneCount; i++) {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }
}
