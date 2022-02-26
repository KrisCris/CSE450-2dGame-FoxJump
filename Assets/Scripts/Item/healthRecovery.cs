using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;

public class healthRecovery : MonoBehaviour
{
    public float recoveryHealthAmount;
    private Vector3 Position;

    private void Start() {
        
    }

    private void Update() {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerEntity>()) {
            // other.GetComponent<PlayerEntity>().OnHealing(recoveryHealthAmount);
            Destroy(gameObject);
        }
    }
}
