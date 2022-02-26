using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using Item;
using UnityEngine;

public class FlyingShoes : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerEntity>()) {
            other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(Items.FlyingShoes, 1);
            Destroy(gameObject);
        }
    }


}
