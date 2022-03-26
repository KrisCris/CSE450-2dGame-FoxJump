using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public Button startGame;

    void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.UnloadSceneAsync("MainUI");
        SceneManager.LoadScene("Scene_0", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        Time.timeScale = 1;
    }
}
