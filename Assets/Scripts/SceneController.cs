using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController instance;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        // SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        // SceneManager.LoadScene("Test_2_hpc", LoadSceneMode.Additive);
        SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
    }

    public void SwitchScene(String scene) {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
