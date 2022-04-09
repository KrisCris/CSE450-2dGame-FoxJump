using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneController : MonoBehaviour
{
    public string scene;
    public GameObject startPoint;
    
    void Start()
    {
        if (startPoint)
        {
            FindObjectOfType<PlayerEntity>().gameObject.transform.position = startPoint.transform.position;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
