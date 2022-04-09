using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenRoom : MonoBehaviour {
    private TilemapCollider2D _tilemapCollider2D;
    // Start is called before the first frame update
    void Start() {
        _tilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }
}
