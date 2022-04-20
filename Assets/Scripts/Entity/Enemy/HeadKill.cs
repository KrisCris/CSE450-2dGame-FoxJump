using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Enemy {
    public class HeadKill : MonoBehaviour {
        public GameObject mob;
        public GameObject explosionVFX;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                KillMob();
            }
        }

        public void KillMob() {
            SoundController.Instance.PlayEnemyCrushed();
            if (explosionVFX) {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
            }
            Destroy(mob);
        }
    }
}

