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
        public Collider2D arenaBound;
        public AudioSource prevAudio;
        public AudioSource bossAudio;
        public string endGameScene;
        private PlayerEntity _player;
        private bool _triggered;
        private bool _attacking;
        private bool _shownDieMessage;

        private void Awake() {
            _triggered = false;
            _attacking = false;
            _shownDieMessage = false;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!_triggered && other.gameObject.GetComponent<PlayerEntity>()) {
                Debug.Log("try trigger boss fight");
                // activate arena bound
                if (arenaBound) {
                    arenaBound.enabled = true;
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
                if (SceneController.Instance) {
                    if (MessageController.Instance) {
                        MessageController.Instance.ShowMessage("Boss defeated...\nTime to go home...",
                            () => { SceneController.Instance.SwitchMap(endGameScene); });
                    } else {
                        SceneController.Instance.SwitchMap(endGameScene);
                    }
                }

                _shownDieMessage = true;
            }
        }
    }
}