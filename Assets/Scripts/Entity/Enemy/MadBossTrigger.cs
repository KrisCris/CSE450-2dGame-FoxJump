using System;
using Entity.Player;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Enemy {
    public class MadBossTrigger : MonoBehaviour {
        private bool _triggered = false;
        private bool _attacking = false;
        public Collider2D cameraBound;
        public Collider2D arenaBound;
        public GameObject boss;
        private PlayerEntity _player;
        public AudioSource prevAudio;
        public AudioSource bossAudio;

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
                    boss.gameObject.AddComponent<Rigidbody2D>();
                    var rb = boss.GetComponent<Rigidbody2D>();
                    // rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.mass = 40000f;
                    var bossController = boss.GetComponent<Boss>();
                    bossController.safeLock = false;
                    bossController.SetAnimationSpeed(5);
                    bossController.touchDamage = (int)(other.gameObject.GetComponent<PlayerEntity>().health * .5);
                    _triggered = true;
                }
            }
        }

        private void FixedUpdate() {
            if (_triggered && !_attacking) {
                if (boss.GetComponent<Rigidbody2D>().velocity.y < 1) {
                    boss.GetComponent<Boss>().StartAttack();
                    
                    // lock camera movement
                    if (cameraBound) {
                        CameraController.Instance.UpdateConfiner(cameraBound);
                    }
                    
                    // disable regular bgm
                    if (prevAudio) {
                        prevAudio.Stop();
                    }
                    
                    // enable boss fight bgm
                    if (bossAudio && !bossAudio.isPlaying) {
                        bossAudio.Play();
                    }
                    
                    // disable death screen
                    _player.showDeath = false;
                    
                    _attacking = true;
                }
            }

            if (_player && _player.health <= 0) {
                if (bossAudio) {
                    bossAudio.Stop();
                }

                if (MessageController.Instance) {
                    MessageController.Instance.ShowMessage("Fainted...\nThe stone giant is too powerful...", () => {
                        SceneController.Instance.SwitchMap("Scene_0_1");
                        _player.SendMessage("OnHealing", _player.maxHealth * .5f);
                    });
                }
            }
            
        }
    }
}