using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        // SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        // SceneManager.LoadScene("Test_2_hpc", LoadSceneMode.Additive);
        SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
    }
}
