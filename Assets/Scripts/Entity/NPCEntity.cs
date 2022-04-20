using System;
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

        [TextArea(3, 10)] public List<string> dialogues;

        private Messages _message;

        public Items sellItem = Items.RecoveryBlood;
        public Items priceItem = Items.Coin;
        public int sellNum = 2;
        public int priceNum = 5;

        private void Awake() {
            if (dialogues.Count < 1) {
                dialogues = new List<string> {
                    "It's you!",
                    "Thanks for releasing me from the god damn chests!",
                    "Be careful, those chests can be really wild!",
                    "Ah, I can heal you and boost your health...\n in a cheaper cost..."
                };
            }

            _message = new Messages(dialogues);
        }

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
            string str = String.Format("Get {0} {1}(s) for {2} {3}(s).", num,
                product == Items.RecoveryBlood ? "Health" : product.ToString(), price,
                spend == Items.RecoveryBlood ? "Health" : spend.ToString());
            _messageController.ShowClickable(
                str,
                (btn) => {
                    if (_player.OnItemUsed(spend, price)) {
                        _player.OnItemCollect(product, num);
                    } else {
                        _messageController.ShowMessage("Not enough...");
                    }
                });
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>()) {
                if (echoSound && !echoSound.isPlaying) {
                    echoSound.Play();
                }

                _messageController.ShowMessage(_message, () => { ShowShop(priceItem, priceNum, sellItem, sellNum); });
            }
        }

        private void OnTriggerStay2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>() && !_messageController.isMessageOn() &&
                !_messageController.isClickableOn()) {
                // if (echoSound && !echoSound.isPlaying) {
                //     echoSound.Play();
                // }

                _messageController.ShowMessage(_message, () => { ShowShop(priceItem, priceNum, sellItem, sellNum); });
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