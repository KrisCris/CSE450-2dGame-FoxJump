using System;
using Entity.Player;
using UnityEngine;

namespace Ground {
    public class HeavyCage : MonoBehaviour, ITriggerable {
        private bool _hasTriggered = false;
        private bool _onGround;
        public AudioSource onGroundAudio;
        public GameObject VFX;

        private Rigidbody2D _rigidbody;

        private void Update() {
            if (_hasTriggered) {
                if (!_onGround && Mathf.Abs(_rigidbody.velocity.y) < 1E-1) {
                    _onGround = true;
                    if (onGroundAudio && !onGroundAudio.isPlaying) {
                        onGroundAudio.Play();
                    }
                    if (VFX) {
                        Instantiate(VFX, transform.position, Quaternion.identity);
                    }
                }
            }
        }

        public void Trigger() {
            if (!_hasTriggered) {
                if (!GetComponent<Rigidbody2D>()) {
                    _rigidbody = gameObject.AddComponent<Rigidbody2D>();
                    _rigidbody.mass = 1000;
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    _hasTriggered = true;
                }
            }
        }

        public bool HasTriggered() {
            return _onGround;
        }
    }
}