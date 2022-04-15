using UnityEngine;

namespace Entity.Enemy {
    public class HeadKill : MonoBehaviour {
        public GameObject mob;
        public AudioClip hitsound;
        public AudioSource audioSource;


        private new void Start()
        {
            
            audioSource = GetComponent<AudioSource>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                SoundController.Instance.PlaySound(hitsound);
                Destroy(mob);
            }
        }
    }
}

