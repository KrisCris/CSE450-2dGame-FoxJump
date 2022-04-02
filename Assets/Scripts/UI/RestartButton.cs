using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    // public Button button;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync("Scene_0");
            SceneManager.UnloadSceneAsync("Player");
            SceneManager.UnloadSceneAsync("info");
            // SceneManager.LoadScene("GamePlay");
            SceneManager.LoadScene("Scene_0", LoadSceneMode.Additive);
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
            Time.timeScale = 1;
        });
    }
}
