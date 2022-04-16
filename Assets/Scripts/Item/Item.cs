using System;
using Entity.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Item {
    public class Item : MonoBehaviour{
        public int frequency;
        public AudioClip soundWhenEaten;
        protected Vector3 Position;
        protected bool IsFloat = true;
        protected Items ItemClass = Items.Default;

        protected SoundController SoundControlInst;

        private void Start() {
            Position = transform.position;
            if (SoundController.Instance) {
                SoundControlInst = SoundController.Instance;
            }
        }

        private void Update() {
            if (IsFloat) {
                transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
            }
        }

        protected void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>() && (!GetComponent<Rigidbody2D>() || GetComponent<Rigidbody2D>().velocity.magnitude < 1f)) {
                other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(ItemClass, 1);
                // SoundControlInst.PlaySound(soundWhenEaten);
                PlayCollectionSound();
                Destroy(gameObject);
            }
        }

        protected virtual void PlayCollectionSound() {
            SoundControlInst.PlaySound(SoundControlInst.defaultItemCollection);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Player") {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
            }
        }
    }
}