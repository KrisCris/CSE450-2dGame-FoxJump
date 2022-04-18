using System;
using Entity.Player;
using UI;
using UnityEngine;

namespace Item {
    public class TreasureChest : MonoBehaviour {
        private Animator _animator;
        private bool _hasOpen;
        private float _startTime;
        private Vector3 _startPosition;
        private bool _inRange;

        public float mass = 1;
        public float force = 150;

        private PlayerEntity _player;

        public GameObject item;

        private void Start() {
            _hasOpen = false;
            _inRange = false;
            _animator = GetComponent<Animator>();
        }

        private void Update() {
            if (_inRange && !_hasOpen && _player) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    if (_player.OnItemUsed(Items.Key, 1)) {
                        // Animation
                        _hasOpen = true;
                        _animator.SetBool("isOpen", _hasOpen);

                        // Init Item
                        GameObject itemInst = Instantiate(item);
                        // Prevent Item from falling
                        BoxCollider2D col = itemInst.AddComponent<BoxCollider2D>();
                        col.size = new Vector2(.5f, .5f);
                        // Prevent collision w/ player
                        Physics2D.IgnoreCollision(_player.gameObject.GetComponent<CapsuleCollider2D>(),
                            GetComponent<BoxCollider2D>());
                        // Let item fall by gravity
                        Rigidbody2D rb;
                        if (!itemInst.GetComponent<Rigidbody2D>()) {
                            rb = itemInst.AddComponent<Rigidbody2D>();
                            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                            rb.mass = mass;
                        } else {
                            rb = itemInst.GetComponent<Rigidbody2D>();
                        }
                        bool isLeft = _player.gameObject.transform.position.x < transform.position.x;
                        rb.AddForce(((isLeft ? 1 : -1) * transform.right + transform.up) * force, ForceMode2D.Force);
                        itemInst.transform.position = transform.position + transform.up * 0.4f;
                        // Some GameState Change
                        if (GameController.Instance) {
                            GameController.Instance.seenChest = true;
                        }
                    } else {
                        if (MessageController.Instance) {
                            MessageController.Instance.ShowMessage("You need a key to open it...");
                        }
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!_hasOpen && col.gameObject.GetComponent<PlayerEntity>()) {
                _inRange = true;
                _player = col.GetComponent<PlayerEntity>();
                if (GameController.Instance && MessageController.Instance && !GameController.Instance.seenChest) {
                    MessageController.Instance.ShowMessage("Press [E] to interact with the chest!");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>()) {
                _inRange = false;
                _player = null;
                if (GameController.Instance && MessageController.Instance) {
                    MessageController.Instance.HideMessage(true);
                }
            }
        }
    }
}