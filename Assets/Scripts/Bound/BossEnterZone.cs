using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterZone : MonoBehaviour {
    public GameObject ToAwake;

    private bool hasEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (!hasEnter && col.gameObject.CompareTag("Player")) {
            ToAwake.SendMessage("StartAttack");
            hasEnter = true;
        }
    }
}
