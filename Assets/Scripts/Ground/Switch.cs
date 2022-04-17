using UnityEngine;

namespace Ground {
    public class Switch : MonoBehaviour
    
    {
        private SpriteRenderer _spriteRenderer;
        
        // Outlet
        public Sprite afterswitch;
        public GameObject[] targets;
        
        // State Track
        public bool isOnSwitch = false;

        // Start is called before the first frame update
        void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update() {
            if (isOnSwitch && Input.GetKey(KeyCode.E)) {
                _spriteRenderer.sprite = afterswitch;
                for (int i = 0; i < targets.Length; i++){
                    if (targets[i]) {
                        targets[i].SendMessage("Triggered");
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                isOnSwitch = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                isOnSwitch = false;
            }
        }
    }
}
