using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float hp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hp);
    }

    private void getDamage(int damage)
    {
        hp -= damage;
    }
}
