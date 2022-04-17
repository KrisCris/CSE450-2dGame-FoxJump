using System;
using System.Collections;
using Entity.Player;
using UnityEngine;

namespace Item {
    public class Item : MonoBehaviour{
        public int frequency = 1;
        public bool isFloat = true;
        public AudioClip destroySound;
        public Items itemClass = Items.Default;
        public GameObject destroyFx;

        protected Vector3 Position;
        
        private void Start() {
            Position = transform.position;
        }

        private void Update() {
            if (isFloat && !GetComponent<Rigidbody2D>()) {
                transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
            }
        }

        protected void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>() && (!GetComponent<Rigidbody2D>() || GetComponent<Rigidbody2D>().velocity.magnitude < 1f)) {
                // Collect
                OnItemCollect(other.gameObject.GetComponent<PlayerEntity>());
                // SFX
                if (SoundController.Instance && destroySound) {
                    SoundController.Instance.PlaySound(destroySound);
                }
                // VFX
                if (destroyFx) {
                    Destroy(Instantiate(destroyFx, transform.position, Quaternion.identity), 1f);
                }
                // Destroy
                Destroy(gameObject);
            }
        }
        protected virtual void OnItemCollect(PlayerEntity player) {
            player.OnItemCollect(itemClass, 1);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Player") {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
            }
        }
    }
}