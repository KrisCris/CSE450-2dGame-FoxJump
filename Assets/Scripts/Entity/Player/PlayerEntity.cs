using System;
using System.Collections.Generic;
using Entity.Enemy;
using Item;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Player {
    public class PlayerEntity : JumpableEntity {
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

        // private Dictionary<Items, int> _inventory;
        // private HashSet<Component> _interactable;

        private bool _climbable;
        private bool _isCrouching;
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int Speed = Animator.StringToHash("Speed");

        public TextMeshProUGUI coinCountText;
        public TextMeshProUGUI keyCountText;

        public AudioSource playerJump;
        public AudioSource playerWalk;

        public float footDistance = 0.3f;
        public float checkDistance = 0.1f;
        public float footYOffset = -0.6f;

        private new void Start() {
            base.Start();
            // _inventory = new Dictionary<Items, int>();
            _isCrouching = false;
            _climbable = false;
            if (GameController.Instance) {
                SetJumps(GameController.Instance.maxJumps);
            }
        }

        protected new void FixedUpdate() {
            base.FixedUpdate();
            // This is synced with Physics Engine

            Animator.SetFloat("HorizontalSpeed", Mathf.Abs(Rigidbody2D.velocity.x));
            Animator.SetBool("Climbable", _climbable);

            // if (Rigidbody2D.velocity.magnitude > 0) {
            //     // Debug.Log(Mathf.Abs(Rigidbody2D.velocity.x) / 2.2f);
            //     Animator.speed = Mathf.Abs(Rigidbody2D.velocity.x) / 2.2f;
            // }
            // else {
            //     Animator.speed = 1f;
            // }
        }

        private new void Update() {
            base.Update();
            Animator.SetFloat("VerticalSpeed", Mathf.Abs(Rigidbody2D.velocity.y));
            if (SceneManager.GetSceneByName("Menu").isLoaded
                || SceneManager.GetSceneByName("info").isLoaded) {
                return;
            }

            if (Input.GetKeyDown(key["attack"])) {
                GameObject newProjectile = Instantiate(projectile);
                newProjectile.GetComponent<DefaultProjectile>().Init(transform.position, 10f, FacingRight);
            }

            if (Input.GetKey(key["up"]) && _climbable) {
                Move(Vector2.up, 20f);
                // TouchingGround = false;
            }

            if (Input.GetKey(key["down"])) {
                _isCrouching = true;
            }

            if (Input.GetKeyUp(key["down"])) {
                _isCrouching = false;
            }

            if (Input.GetKey(key["left"])) {
                Move(Vector2.left);
            }

            if (Input.GetKey(key["right"])) {
                Move(Vector2.right);
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
            if (Mathf.Abs(Rigidbody2D.velocity.x) > 0 && IsGrounded) {
                if (!playerWalk.isPlaying) {
                    playerWalk.Play();
                }
            } else {
                playerWalk.Stop();
            }
        }

        protected override bool PerformJump(bool free = false) {
            if (base.PerformJump(free) && !playerJump.isPlaying) {
                playerJump.Play();
                return true;
            }
            return false;
        }

        public Vector2 GetFrontFoot() {
            float foreX = GetX() + (footDistance * GetFaceDirection());
            Vector2 foreFoot = new Vector2(foreX, GetY() + footYOffset);
            return foreFoot;
        }

        public Vector2 GetBackFoot() {
            float behindX = GetX() - ((footDistance + 0.15f) * GetFaceDirection());
            Vector2 behindFoot = new Vector2(behindX, GetY() + footYOffset);
            return behindFoot;
        }

        // public Dictionary<Items, int> GetInventory() {
        //     return _inventory;
        // }

        public void OnItemCollect(Items item, int num) {
            if (item == Items.Coin) {
                GameController.Instance.AddCoins(num);
                coinCountText.text = GameController.Instance.coins.ToString();
            }

            if (item == Items.Key) {
                GameController.Instance.AddKeys(num);
                keyCountText.text = GameController.Instance.keys.ToString();
            }

            if (item == Items.FlyingShoes) {
                UpdateMaxJump(num);
            }
        }

        public bool OnItemUsed(Items item, int num) {
            if (item == Items.Coin) {
                if (GameController.Instance.coins >= num) {
                    GameController.Instance.AddCoins(-num);
                    coinCountText.text = GameController.Instance.coins.ToString();
                    return true;
                }
            }

            if (item == Items.Key) {
                if (GameController.Instance.keys >= num) {
                    GameController.Instance.AddKeys(-num);
                    keyCountText.text = GameController.Instance.keys.ToString();
                    return true;
                }
            }

            return false;
        }

        protected override void OnDeath(string reason) {
            // Time.timeScale = 0;
            SceneManager.LoadScene("Info", LoadSceneMode.Additive);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) {
                _climbable = true;
            }

            if (other.gameObject.GetComponent<HeadKill>()) {
                PerformJump(true);
                Animator.SetBool("IsJumping", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) {
                _climbable = false;
            }
        }
    }
}