using System;
using Entity.Player;
using UnityEngine;

namespace Item {
    public class Item : MonoBehaviour{
        public int frequency;
        protected Vector3 Position;
        protected bool IsFloat = true;
        protected Items ItemClass = Items.Default;

        private void Start() {
            Position = transform.position;
        }

        private void Update() {
            if (IsFloat) {
                transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(ItemClass, 1);
                Destroy(gameObject);
            }
        }
    }
}