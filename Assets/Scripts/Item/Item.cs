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
        private bool _avoidPlayerCollision;
        
        private void Start() {
            Position = transform.position;
        }

        private void Update() {
            if (isFloat && !GetComponent<Rigidbody2D>()) {
                transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
            }

            if (GetComponent<BoxCollider2D>() && FindObjectOfType<PlayerEntity>() && !_avoidPlayerCollision) {
                // Prevent collision w/ player
                Physics2D.IgnoreCollision(FindObjectOfType<PlayerEntity>().gameObject.GetComponent<CapsuleCollider2D>(),
                    GetComponent<BoxCollider2D>());
                _avoidPlayerCollision = true;
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
    }
}