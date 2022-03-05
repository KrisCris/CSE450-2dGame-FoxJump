using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
	public Button startGame;
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(StartGame); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame() {
        SceneManager.UnloadSceneAsync("MainUI");
        SceneManager.LoadScene("UI");
        SceneManager.LoadScene("Test_2_hpc", LoadSceneMode.Additive); 
    }
}
