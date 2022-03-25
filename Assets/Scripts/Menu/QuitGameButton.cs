using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    public Button quitGameButton;
    void Start()
    {
        quitGameButton.onClick.AddListener(quitGame);
    }

    void quitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}