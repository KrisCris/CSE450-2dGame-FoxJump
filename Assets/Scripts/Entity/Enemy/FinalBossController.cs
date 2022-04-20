using System.Collections.Generic;
using Entity.Player;
using Ground;
using UI;
using UnityEngine;

namespace Entity.Enemy {
    public class FinalBossController : MonoBehaviour {
        public List<GameObject> triggerableItems;
        public Boss boss;
        public Collider2D cameraBound;
        public Collider2D arenaBoundL;
        public Collider2D arenaBoundR;
        public AudioSource prevAudio;
        public AudioSource bossAudio;
        public string endGameScene;

        public GameObject ladder;
        public AudioSource ladderSound;
        
        private PlayerEntity _player;
        private bool _triggered;
        private bool _attacking;
        private bool _shownDieMessage;

        private void Awake() {
            _triggered = false;
            _attacking = false;
            _shownDieMessage = false;
            ladder.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!_triggered && other.gameObject.GetComponent<PlayerEntity>()) {
                Debug.Log("try trigger boss fight");
                // activate arena bound
                if (arenaBoundL) {
                    arenaBoundL.enabled = true;
                }

                if (arenaBoundR) {
                    arenaBoundR.enabled = true;
                }

                // Init Boss
                _player = other.gameObject.GetComponent<PlayerEntity>();
                if (boss && _player) {
                    if (cameraBound) {
                        CameraController.Instance.UpdateConfiner(cameraBound);
                    }

                    if (prevAudio) {
                        prevAudio.Stop();
                    }

                    foreach (GameObject triggerableItem in triggerableItems) {
                        if (!triggerableItem.GetComponent<ITriggerable>().HasTriggered()) {
                            triggerableItem.GetComponent<ITriggerable>().Trigger();
                        }
                    }

                    _triggered = true;
                }
            }
        }

        private void LateUpdate() {
            if (_triggered && !_attacking) {
                foreach (GameObject triggerableItem in triggerableItems) {
                    if (triggerableItem.GetComponent<ITriggerable>().HasTriggered()) {
                        boss.GetComponent<Boss>().StartAttack();
                        if (bossAudio && !bossAudio.isPlaying) {
                            bossAudio.Play();
                        }
                        _attacking = true;
                        break;
                    }
                }
            }

            if (boss.GetComponent<Boss>().IsDead() && !_shownDieMessage) {
                arenaBoundR.enabled = false;
                // bossAudio.Stop();
                if (SceneController.Instance) {
                    if (MessageController.Instance) {
                        MessageController.Instance.ShowMessage("Boss defeated...\nTime to go home...",
                            () => {
                                if (ladder) {
                                    // Enable Ladder
                                    ladder.SetActive(true);
                                    // Avoid Player Collision
                                    Physics2D.IgnoreCollision(_player.gameObject.GetComponent<CapsuleCollider2D>(),
                                        ladder.GetComponent<BoxCollider2D>());
                                    // Let it naturally fall down to the ground
                                    var rb = ladder.AddComponent<Rigidbody2D>();
                                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                                    rb.mass = 1000;
                                    // Try play a sound
                                    if (ladderSound && !ladderSound.isPlaying) {
                                        ladderSound.Play();
                                    }
                                } else {
                                    SceneController.Instance.SwitchMap(endGameScene);
                                }
                            });
                    } else {
                        SceneController.Instance.SwitchMap(endGameScene);
                    }
                }

                _shownDieMessage = true;
            }
        }
    }
}