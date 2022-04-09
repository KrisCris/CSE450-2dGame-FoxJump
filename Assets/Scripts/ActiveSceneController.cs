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
            var position = startPoint.transform.position;
            FindObjectOfType<PlayerEntity>().gameObject.transform.position = new Vector3 (
                position.x, 
                position.y, 
                0
            );
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
