using Entity.Player;
using UI;
using UnityEngine;

namespace Ground {
    public class SavePoint : MonoBehaviour {
        public AudioSource interactSound;

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>() && GameController.Instance) {
                if (!interactSound.isPlaying) {
                    interactSound.Play();
                }

                col.gameObject.GetComponent<PlayerEntity>().OnHealing(30);
                
                GameController.Instance.SetLastSavePoint(GetSavePoint());
                if (MessageController.Instance) {
                    MessageController.Instance.ShowMessage("Game Saved");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (MessageController.Instance && MessageController.Instance.isMessageOn()) {
                MessageController.Instance.HideMessage();
            }
        }

        private Vector2 GetSavePoint() {
            return gameObject.transform.position;
        }
    }
}