using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Enemy {
    public class HeadKill : MonoBehaviour {
        public GameObject mob;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                SoundController.Instance.PlaySound(SoundController.Instance.enemyCrushed);
                Destroy(mob);
            }
        }
    }
}

