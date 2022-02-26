using Entity.Player;
using UnityEngine;

namespace Item {
    public class TreasureChest : MonoBehaviour {
        private Animator animator;
        private bool isOpening = false;
        private bool hasOpen = false;
        private bool isLeft;
        private float startTime;
        private Vector3 startPosition;
        private GameObject newitem;

        public GameObject item;
        public float throwTime;
        public float throwDistance;
        
        // Start is called before the first frame update
        private void Start() {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update() {
            if (isOpening && Time.fixedTime < startTime + throwTime) {
                newitem.transform.position = startPosition + (isLeft ? 1 : -1) * (0.5f * (1 - Mathf.Cos((Time.fixedTime-startTime) / throwTime * Mathf.PI)) * throwDistance * transform.right
                    + 0.5f * Mathf.Sin((Time.fixedTime-startTime) / throwTime * Mathf.PI) * throwDistance * transform.up);
            }
            else {
                if (isOpening) {
                    isOpening = false;
                    newitem.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (! hasOpen && other.gameObject.GetComponent<PlayerEntity>()) {
                animator.SetBool("isOpen", true);
                isOpening = true;
                hasOpen = true;
                newitem = Instantiate(item);
                newitem.GetComponent<BoxCollider2D>().enabled = false;
                startPosition = transform.position + transform.up * 0.4f;
                newitem.transform.position = startPosition;
                isLeft = other.transform.position.x < transform.position.x;
                startTime = Time.fixedTime;
            }
        }
    }
}