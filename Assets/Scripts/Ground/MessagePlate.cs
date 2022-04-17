using Entity.Player;
using UI;
using UnityEngine;

namespace Ground {
    public class MessagePlate : MonoBehaviour {
        public string message;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.GetComponent<PlayerEntity>() && message.Length > 0 && MessageController.Instance) {
                MessageController.Instance.ShowMessage(message);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.GetComponent<PlayerEntity>() && message.Length > 0 && MessageController.Instance) {
                MessageController.Instance.HideMessage();
            }
        }
    }
}