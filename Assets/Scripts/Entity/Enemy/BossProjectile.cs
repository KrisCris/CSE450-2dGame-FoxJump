using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    // Outlet
    private Animator _animator;
    public float speed;
    
    // Start is called before the first frame update
    void Start() {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
