using System;
using System.Collections.Generic;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Player {
    public class PlayerEntity : JumpableEntity {
        // public KeyCode keyUp = KeyCode.W;
        // public KeyCode keyDown = KeyCode.S;
        // public KeyCode keyLeft = KeyCode.A;
        // public KeyCode keyRight = KeyCode.D;
        // public KeyCode keyJump = KeyCode.Space;
        // public KeyCode keyInteraction = KeyCode.E;
        // public KeyCode keyAttack = KeyCode.Mouse0;
        public Dictionary<string, KeyCode> key = new() {
            {"up", KeyCode.W},
            {"down", KeyCode.S},
            {"left", KeyCode.A},
            {"right", KeyCode.D},
            {"jump", KeyCode.Space},
            {"interaction", KeyCode.E},
            {"attack", KeyCode.Mouse0}
        };

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
            Animator.SetFloat("HorizontalSpeed", Mathf.Abs(Rigidbody2D.velocity.x));
            if (Rigidbody2D.velocity.magnitude > 0) {
                // Debug.Log(Mathf.Abs(Rigidbody2D.velocity.x) / 2.2f);
                Animator.speed = Mathf.Abs(Rigidbody2D.velocity.x) / 2.2f;
            }
            else {
                Animator.speed = 1f;
            }
        }

        private new void Update() {
            base.Update();
            if (SceneManager.GetSceneByName("Menu").isLoaded
                || SceneManager.GetSceneByName("info").isLoaded) {
                return;
            }

            if (Input.GetKeyDown(key["attack"])) {
                GameObject newProjectile = Instantiate(projectile);
                newProjectile.GetComponent<DefaultProjectile>().Init(transform.position, 10f, FacingRight);
            }

            if (Input.GetKey(key["up"])) {
                // TODO climb?
            }

            if (Input.GetKey(key["down"])) {
                _isCrouching = true;
            }

            if (Input.GetKeyUp(key["down"])) {
                _isCrouching = false;
            }

            if (Input.GetKey(key["left"])) {
                Move(Vector2.left);
                // if (FacingRight) {
                //     FlipFacing();
                // }
                // Rigidbody2D.AddForce(Vector2.left * (12f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(key["right"])) {
                Move(Vector2.right);
                // if (!FacingRight) {
                //     FlipFacing();
                // }
                //
                // Rigidbody2D.AddForce(Vector2.right * (12f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(key["jump"])) {
                PerformJump();
            }
            
            //TODO Move to somewhere else
            if (Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetSceneByName("Menu").isLoaded) {
                Time.timeScale = 0;
                SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
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

        protected override void OnDeath(string reason) {
            // Time.timeScale = 0;
            SceneManager.LoadScene("Info", LoadSceneMode.Additive);
        }
    }
}