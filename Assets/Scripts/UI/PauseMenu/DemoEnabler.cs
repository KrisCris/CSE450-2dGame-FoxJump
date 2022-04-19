using Entity.Player;
using UnityEngine;

namespace UI.PauseMenu {
    public class DemoEnabler : MonoBehaviour {
        private void OnTriggerStay2D(Collider2D other) {
            if (Input.GetKey(KeyCode.E)) {
                if (other.GetComponent<PlayerEntity>()) {
                    if (GameController.Instance) {
                        GameController.Instance.DemoMode();
                    }

                    if (MessageController.Instance) {
                        MessageController.Instance.ShowMessage("DEMO MENU ENABLED!!!");
                    }
                }
            }
        }
    }
}