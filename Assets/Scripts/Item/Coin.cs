using Entity.Player;
using UnityEngine;

namespace Item {
    public class Coin : MonoBehaviour {
        public int frequency;
        private Vector3 Position;

        private void Start() {
            Position = transform.position;
        }

        private void Update() {
            transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(Items.Coin, 1);
                Destroy(gameObject);
            }
        }
    }
}