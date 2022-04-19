using Entity.Player;
using UI;
using UnityEngine;

namespace Ground {
    public class SavePoint : MonoBehaviour {
        public AudioSource interactSound;
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>()) {
                if (col.gameObject.GetComponent<PlayerEntity>().OnHealing(1000) && !interactSound.isPlaying) {
                    interactSound.Play();
                }

                if (GameController.Instance) {
                    if (GameController.Instance.GetLastSavePoint() != GetSavePoint()) {
                        GameController.Instance.SetLastSavePoint(GetSavePoint());
                        if (MessageController.Instance) {
                            MessageController.Instance.ShowMessage("Game Saved");
                        }
                    }
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