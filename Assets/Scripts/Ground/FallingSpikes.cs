using System;
using UnityEngine;

namespace Ground {
    public class FallingSpikes : MonoBehaviour, ITriggerable {
        private bool _triggered = false;
        public AudioSource fallingAudio;
        public void Trigger() {
            if (!_triggered) {
                if (!gameObject.GetComponent<Rigidbody2D>()) {
                    var rb = gameObject.AddComponent<Rigidbody2D>();
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.mass = 100;
                    if (fallingAudio && !fallingAudio.isPlaying) {
                        fallingAudio.Play();
                    }
                    _triggered = true;
                }
            }

        }

        public bool HasTriggered() {
            return _triggered;
        }

        private void OnBecameInvisible() {
            if (_triggered) {
                Destroy(gameObject);
            }
        }
    }
}