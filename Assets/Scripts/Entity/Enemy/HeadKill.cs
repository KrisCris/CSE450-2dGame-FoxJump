using UnityEngine;

namespace Entity.Enemy {
    public class HeadKill : MonoBehaviour {
        public GameObject mob;
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Destroy(mob);
            }
        }
    }
}

