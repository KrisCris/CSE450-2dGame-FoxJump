using System;
using UnityEngine;

namespace Entity.Enemy {
    public class BossProjectile : MonoBehaviour {
        // OutLet
        private Rigidbody2D _rigidbody2D;
        private Transform _target;
        [SerializeField] private float shotSpeed;
        public float attackDamage = 2;
        [SerializeField] private float maxLife = 2.0f;
        public bool damageable = true;
        public int timer = 25;
        private float _lifeBtwTimer;
        public GameObject destroyEffect;
        public float health;
        private int _flashingCountdown = 0;
        private SpriteRenderer _spriteRenderer;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _rigidbody2D.velocity = new Vector2(-shotSpeed / 2f, 0);
            health = maxLife;
        }

        void Update() {
            float acceleration = shotSpeed / 80f;
            float maxSpeed = shotSpeed;

            // Home in on target
            Vector2 directionToTarget = _target.position - transform.position;
            _rigidbody2D.MoveRotation(Mathf.Atan2(-_rigidbody2D.velocity.y, -_rigidbody2D.velocity.x) * Mathf.Rad2Deg);
            // Accelerate
            _rigidbody2D.AddForce(directionToTarget * acceleration);

            // Cap max speed
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, maxSpeed);
        }

        private void LateUpdate() {
            if (_flashingCountdown > 0) {
                --_flashingCountdown;
            }

            if (_flashingCountdown == 0) {
                _spriteRenderer.color = Color.white;
            }
        }

        protected void FixedUpdate() {
            if (timer == 0) {
                damageable = true;
                timer = 25;
            }

            if (!damageable && timer > 0) {
                --timer;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player")) {
                collision.gameObject.SendMessage("OnDamage", attackDamage);
                OnDestroy();
            }
        }

        public void OnDamage(float dmg) {
            if (damageable) {
                health = Mathf.Max(health - dmg, 0);
                damageable = false;
                if (health <= 0) {
                    OnDestroy();
                }
            }
        }

        private void OnDestroy() {
            if (GameObject.FindGameObjectWithTag("Boss")) {
                var bossController = GameObject.FindGameObjectWithTag("Boss").gameObject.GetComponent<Boss>();
                if (bossController) {
                    bossController.PlayArmDestroySound();

                    if (destroyEffect) {
                        Instantiate(destroyEffect, transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject);
                    bossController.SendMessage("ArmAttackFinished");
                }
            } else {
                if (destroyEffect) {
                    Instantiate(destroyEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}