using System;
using Entity.Player;
using UnityEngine;

namespace Item {
    public class Bomb : MonoBehaviour {
        public GameObject destroyFx;
        public int countdown = 3;
        public float counter = 0;
        public AudioClip explode;

        private PlayerEntity _player;

        private void Update() {
            counter += Time.deltaTime;
            if (counter >= countdown) {
                if (explode && SoundController.Instance) {
                    SoundController.Instance.PlaySound(explode);
                }

                if (destroyFx) {
                    Instantiate(destroyFx, transform.position, Quaternion.identity);
                }
                
                if (_player) {
                    _player.SendMessage("OnDamage", 8);
                }
                
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                _player = other.gameObject.GetComponent<PlayerEntity>();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                _player = null;
            }
        }
    }
}