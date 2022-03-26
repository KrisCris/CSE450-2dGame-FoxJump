using System;
using Entity.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour {
    public string sceneName;
    private Collider2D _collider2D;

    private void Start() {
        if (!_collider2D) {
            _collider2D = GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<PlayerEntity>()) {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
