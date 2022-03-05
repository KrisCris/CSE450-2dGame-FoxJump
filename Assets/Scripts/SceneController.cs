using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        // SceneManager.LoadScene("Test_2_hpc", LoadSceneMode.Additive);
        SceneManager.LoadScene("MainUI", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
