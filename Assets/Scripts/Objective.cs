using System;
using Entity.Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour {
    public string sceneName;
    public bool requireKeyInteraction = false;
    private Collider2D _collider2D;
    public bool recoverHealth = false;

    private void Start() {
        if (!_collider2D) {
            _collider2D = GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (requireKeyInteraction && MessageController.Instance) {
            MessageController.Instance.ShowMessage("Press [E] to interact with it...");
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!requireKeyInteraction || Input.GetKey(KeyCode.E)) {
            if (other.GetComponent<PlayerEntity>()) {
                if (MessageController.Instance) {
                    MessageController.Instance.HideMessage(true);
                    MessageController.Instance.HideClickable();
                }
                SceneController.Instance.SwitchMap(sceneName);
                if (recoverHealth) {
                    other.GetComponent<PlayerEntity>().SendMessage("OnHealing", other.GetComponent<PlayerEntity>().maxHealth * .5f);
                }
                
            }
        }

    }
}
