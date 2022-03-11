using System;
using System.Collections.Generic;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Player {
    public class PlayerEntity : JumpableEntity {
        public KeyCode keyUp = KeyCode.W;
        public KeyCode keyDown = KeyCode.S;
        public KeyCode keyLeft = KeyCode.A;
        public KeyCode keyRight = KeyCode.D;
        public KeyCode keyJump = KeyCode.Space;
        public KeyCode keyInteraction = KeyCode.E;
        public KeyCode keyAttack = KeyCode.Mouse0;

        public GameObject projectile;

        private Dictionary<Items, int> _inventory;
        private HashSet<Component> _interactable;

        private bool _isCrouching;
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int Speed = Animator.StringToHash("Speed");

        public TextMeshProUGUI coinCountText;
        public TextMeshProUGUI keyCountText;

        private new void Start() {
            base.Start();
            _inventory = new Dictionary<Items, int>();
            _isCrouching = false;
        }

        protected new void FixedUpdate() {
            base.FixedUpdate();
            // This is synced with Physics Engine
            Animator.SetFloat(Speed, Rigidbody2D.velocity.magnitude);
            if (Rigidbody2D.velocity.magnitude > 0) {
                Animator.speed = Rigidbody2D.velocity.magnitude / 3f;
            }
            else {
                Animator.speed = 1f;
            }
        }

        private new void Update() {
            base.Update();

            if (Input.GetKeyDown(keyAttack)) {
                GameObject newProjectile = Instantiate(projectile);
                newProjectile.transform.position = transform.position;
            }
            
            if (Input.GetKey(keyUp)) {
                // TODO climb?
            }

            if (Input.GetKey(keyDown)) {
                // TODO climb down?
                // TODO crouch?
                _isCrouching = true;
                // TODO maybe some skills
            }

            if (Input.GetKeyUp(keyDown)) {
                _isCrouching = false;
            }

            if (Input.GetKey(keyLeft)) {
                if (FacingRight) {
                    FlipFacing();
                }

                Rigidbody2D.AddForce(Vector2.left * (12f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(keyRight)) {
                if (!FacingRight) {
                    FlipFacing();
                }

                Rigidbody2D.AddForce(Vector2.right * (12f * Time.deltaTime), ForceMode2D.Impulse);
            }

            // jump
            if (Input.GetKeyDown(keyJump)) {
                if (CurrJumps > 0) {
                    --CurrJumps;
                    Rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }

            Animator.SetBool(IsCrouching, _isCrouching);
        }

        public Dictionary<Items, int> GetInventory() {
            return _inventory;
        }

        public void OnItemCollect(Items item, int num) {
            if (!_inventory.ContainsKey(item)) {
                _inventory[item] = 0;
            }
            _inventory[item] += num;
            if (item == Items.Coin) {
                coinCountText.text = "" + _inventory[item];
            }
            if (item == Items.Key) {
                keyCountText.text = "" + _inventory[item];
            }
            if (item == Items.FlyingShoes) {
                UpdateMaxJump(num);
            }
        }

        public bool OnItemUsed(Items item, int num) {
            if (_inventory.ContainsKey(item) && _inventory[item] >= num) {
                _inventory[item] -= num;
                
                if (item == Items.Coin) {
                    coinCountText.text = "" + _inventory[item];
                }
                if (item == Items.Key) {
                    keyCountText.text = "" + _inventory[item];
                }
                return true;
            }
            return false;
        }

        protected override void OnDeath(string reason)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
    }
}