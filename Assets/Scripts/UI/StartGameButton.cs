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
        // while (true)
        // {
        //     if (SceneManager.GetSceneByName("Scene_0").isLoaded)
        //     {
        //         SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene_0"));
        //         break;
        //     }
        // }
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene_0"));

        Time.timeScale = 1;
    }
}
