using Entity.Player;
using UnityEngine;

namespace Item {
    public class TreasureChest : MonoBehaviour {
        private Animator _animator;
        private bool _hasOpen = false;
        private float _startTime;
        private Vector3 _startPosition;

        public GameObject item;

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!_hasOpen && other.gameObject.GetComponent<PlayerEntity>()) {
                if (other.GetComponent<PlayerEntity>().OnItemUsed(Items.Key, 1)) {
                    // Animation
                    _hasOpen = true;
                    _animator.SetBool("isOpen", _hasOpen);
                    // Init
                    GameObject itemInst = Instantiate(item);
                    BoxCollider2D col = itemInst.AddComponent<BoxCollider2D>();
                    col.size = new Vector2(.5f, .5f);
                    Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(),
                        GetComponent<BoxCollider2D>());
                    Rigidbody2D rb = itemInst.AddComponent<Rigidbody2D>();
                    bool isLeft = other.transform.position.x < transform.position.x;
                    rb.AddForce(((isLeft ? 1 : -1) * transform.right + transform.up) * 150f, ForceMode2D.Force);
                    itemInst.transform.position = transform.position + transform.up * 0.4f;
                }
            }
        }
    }
}