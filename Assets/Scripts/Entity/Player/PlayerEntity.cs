using System;
using System.Collections.Generic;
using Entity.Enemy;
using Ground;
using Item;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Player {
    public class PlayerEntity : JumpableEntity {
        public BoxCollider2D eventTrigger;

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
        public TextMeshProUGUI projectileCountText;

        public GameObject hudCoin;
        public GameObject hudKey;
        public GameObject hudProjectile;
        public GameObject hudJumpShoes;
        public GameObject hudFoxTail;

        public AudioSource playerJump;
        public AudioSource playerWalk;
        public AudioSource playerDeath;
        public AudioSource playerHurt;
        public AudioSource playerStrengthen;

        public float footDistance = 0.5f;
        public float checkDistance = 0.1f;
        public float footYOffset = -0.6f;
        public bool showDeath = true;
        public float wallCheckDistance = 0.4f;
        public float wallJumpXForce = .5f;
        public float wallJumpYForce = 1f;

        public int projectileDischargeInterval = 30;
        public int projectileDischargeCooldown = 0;

        private GameController _gameController;

        private new void Start() {
            base.Start();

            _isCrouching = false;
            _climbable = false;

            hudCoin.SetActive(false);
            hudKey.SetActive(false);
            hudProjectile.SetActive(false);
            hudJumpShoes.SetActive(false);
            hudFoxTail.SetActive(false);

            // sync w/ GameController
            if (GameController.Instance) {
                _gameController = GameController.Instance;

                if (_gameController.hasJumpShoes) {
                    SetJumps(2);
                    hudJumpShoes.SetActive(true);
                } else {
                    SetJumps(1);
                }

                coinCountText.text = _gameController.coins.ToString();
                hudCoin.SetActive(_gameController.hasCoin);

                keyCountText.text = _gameController.keys.ToString();
                hudKey.SetActive(_gameController.hasKey);

                projectileCountText.text = _gameController.projectiles.ToString();
                hudProjectile.SetActive(_gameController.hasProjectile);

                hudFoxTail.SetActive(_gameController.hasFoxTail);
            }
        }

        protected new void FixedUpdate() {
            base.FixedUpdate();

            Animator.SetFloat("HorizontalSpeed", Mathf.Abs(Rigidbody2D.velocity.x));
            Animator.SetBool("Climbable", _climbable);

            if (projectileDischargeCooldown > 0) {
                --projectileDischargeCooldown;
            }

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

            if (Input.GetKeyDown(key["attack"]) && !MessageController.Instance.isClickableOn()) {
                if (_gameController.hasProjectile && _gameController.projectiles > 0 &&
                    projectileDischargeCooldown <= 0) {
                    GameObject newProjectile = Instantiate(projectile);
                    newProjectile.GetComponent<DefaultProjectile>().Init(transform.position, 10f, FacingRight, this);
                    OnItemUsed(Items.Projectile, 1);
                    projectileDischargeCooldown = projectileDischargeInterval;
                }
            }

            if (Input.GetKey(key["up"]) && _climbable) {
                Move(Vector2.up, 20f);
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

            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I)) && !SceneManager.GetSceneByName("Menu").isLoaded) {
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

        protected override bool PerformJump(bool free = false, float directionX = 0f, float directionY = 1f,
            int freeReward = 0) {
            var hit = RayCastHelper.RayCast(
                Rigidbody2D.transform.position, FacingRight ? Vector2.right : Vector2.left, wallCheckDistance,
                "Ground");
            if (hit && _gameController.hasFoxTail) {
                if (base.PerformJump(true, FacingRight ? -wallJumpXForce : wallJumpXForce, wallJumpYForce)) {
                    if (!playerJump.isPlaying) {
                        playerJump.Play();
                    }

                    return true;
                }
            } else {
                if (base.PerformJump(free)) {
                    if (!playerJump.isPlaying) {
                        playerJump.Play();
                    }

                    return true;
                }
            }

            return false;
        }

        public Vector2 GetFrontFoot() {
            float foreX = GetX() + (footDistance * GetFaceDirection());
            Vector2 foreFoot = new Vector2(foreX, GetY() + footYOffset);
            return foreFoot;
        }

        public Vector2 GetBackFoot() {
            float behindX = GetX() - (footDistance * GetFaceDirection());
            Vector2 behindFoot = new Vector2(behindX, GetY() + footYOffset);
            return behindFoot;
        }


        public void OnItemCollect(Items item, int num) {
            if (item == Items.Coin) {
                _gameController.coins += num;
                if (!_gameController.hasCoin) {
                    _gameController.hasCoin = true;
                    hudCoin.SetActive(true);
                }

                coinCountText.text = _gameController.coins.ToString();
            }

            if (item == Items.Key) {
                _gameController.keys += num;
                if (!_gameController.hasKey) {
                    _gameController.hasKey = true;
                    hudKey.SetActive(true);
                }

                keyCountText.text = _gameController.keys.ToString();
            }

            if (item == Items.Projectile) {
                _gameController.projectiles += num;
                if (!_gameController.hasProjectile) {
                    _gameController.hasProjectile = true;
                    hudProjectile.SetActive(true);
                }

                projectileCountText.text = _gameController.projectiles.ToString();
            }

            if (item == Items.FlyingShoes && !_gameController.hasJumpShoes) {
                _gameController.hasJumpShoes = true;
                hudJumpShoes.SetActive(true);
                UpdateMaxJump(1);
            }

            if (item == Items.FoxTail && !_gameController.hasFoxTail) {
                _gameController.hasFoxTail = true;
                hudFoxTail.SetActive(true);
            }
            
            
            if (item == Items.RecoveryBlood) {
                OnBoostHealth(num);
            }
        }

        public void SetEventTrigger(bool state) {
            eventTrigger.enabled = state;
        }
        
        public bool OnBoostHealth(float heal) {
            maxHealth += heal;
            if (playerStrengthen && !playerStrengthen.isPlaying) {
                playerStrengthen.Play();
            }
            OnHealing(heal);
            return true;
        }

        public bool OnItemUsed(Items item, int num) {
            if (item == Items.Coin) {
                if (_gameController.coins >= num) {
                    _gameController.coins -= num;
                    coinCountText.text = _gameController.coins.ToString();
                    return true;
                }
            }

            if (item == Items.Key) {
                if (_gameController.keys >= num) {
                    _gameController.keys -= num;
                    keyCountText.text = _gameController.keys.ToString();
                    return true;
                }
            }

            if (item == Items.Projectile) {
                if (_gameController.projectiles >= num) {
                    _gameController.projectiles -= num;
                    projectileCountText.text = _gameController.projectiles.ToString();
                    return true;
                }
            }
            
            
            if (item == Items.RecoveryBlood) {
                OnDamage(num);
                return true;
            }

            return false;
        }

        protected override void OnDeath(string reason) {
            Animator.SetBool("IsDead", true);
            // Time.timeScale = 0;
            if (playerDeath && !playerDeath.isPlaying) {
                playerDeath.Play();
            }

            // SetEventTrigger(true);
            SetColliderState(false);
            if (showDeath) {
                SceneManager.LoadScene("Info", LoadSceneMode.Additive);
            }
        }

        public override bool OnDamage(float dmg) {
            if (base.OnDamage(dmg)) {
                if (playerHurt && !playerHurt.isPlaying) {
                    playerHurt.Play();
                }

                return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) {
                _climbable = true;
            }

            if (other.gameObject.GetComponent<HeadKill>()) {
                PerformJump(true, freeReward: 1);
            }

            if (other.gameObject.GetComponentInParent<Lift>()) {
                Animator.SetBool("OnLift", true);
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.GetComponentInParent<Lift>()) {
                Animator.SetBool("OnLift", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ladder")) {
                _climbable = false;
            }

            if (other.gameObject.GetComponentInParent<Lift>()) {
                Animator.SetBool("OnLift", false);
            }
        }

        public void UnsetClimbable() {
                _climbable = false;
        }
    }
}