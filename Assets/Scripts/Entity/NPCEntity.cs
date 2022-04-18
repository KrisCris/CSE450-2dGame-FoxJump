using System.Collections.Generic;
using Entity.Player;
using Item;
using UI;
using UnityEngine;

namespace Entity {
    public class NPCEntity : Entity {
        public AudioSource echoSound;
        private MessageController _messageController;
        private PlayerEntity _player;

        private Messages _message = new Messages(new List<string> {
            "...",
            "A new face...\nIt's dangerous outside, return home...",
            "No? OK... I am Joe, an adventurer...",
            "If you want to stay here, I have something to offer..."
        });

        protected new void Start() {
            base.Start();
            if (MessageController.Instance) {
                _messageController = MessageController.Instance;
            }

            if (FindObjectOfType<PlayerEntity>()) {
                _player = FindObjectOfType<PlayerEntity>();
                Physics2D.IgnoreCollision(_player.gameObject.GetComponent<CapsuleCollider2D>(),
                    GetComponent<CapsuleCollider2D>());
            }
        }

        private void ShowShop(Items spend, int price, Items product, int num) {
            _messageController.ShowClickable(
                "Get " + num + " boomerangs for " + price + " coin",
                (btn) => {
                    if (_player.OnItemUsed(spend, price)) {
                        _player.OnItemCollect(product, num);
                    } else {
                        _messageController.ShowMessage("Not enough... Go find more coins!");
                    }
                });
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>()) {
                if (echoSound && !echoSound.isPlaying) {
                    echoSound.Play();
                }

                _messageController.ShowMessage(_message, () => { ShowShop(Items.Coin, 1, Items.Projectile, 5); });
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.GetComponent<PlayerEntity>()) {
                if (_messageController.isMessageOn()) {
                    _messageController.HideMessage(true);
                }

                if (_messageController.isClickableOn()) {
                    _messageController.HideClickable();
                    _messageController.ShowMessage("See you later...");
                    if (echoSound && !echoSound.isPlaying) {
                        echoSound.Play();
                    }
                }
            }

        }

        private void OnCollisionEnter2D(Collision2D col) { }
    }
}