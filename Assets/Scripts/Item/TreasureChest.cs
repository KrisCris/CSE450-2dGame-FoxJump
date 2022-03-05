using Entity.Player;
using UnityEngine;

namespace Item {
    public class TreasureChest : MonoBehaviour {
        private Animator animator;
        private bool hasOpen = false;
        private bool isLeft;
        private float startTime;
        private Vector3 startPosition;
        private GameObject newitem;

        public GameObject item;

        // Start is called before the first frame update
        private void Start() {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update() {
            
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!hasOpen && other.gameObject.GetComponent<PlayerEntity>()) {
                if (other.GetComponent<PlayerEntity>().OnItemUsed(Items.Key, 1)) {
                    animator.SetBool("isOpen", true);
                    newitem = Instantiate(item);
                    isLeft = other.transform.position.x < transform.position.x;
                    newitem.GetComponent<Rigidbody2D>().AddForce(((isLeft?1:-1)*transform.right+transform.up)*150f, ForceMode2D.Force);
                    newitem.transform.position = transform.position + transform.up * 0.4f;
                    hasOpen = true;
                }
            }
        }
    }
}