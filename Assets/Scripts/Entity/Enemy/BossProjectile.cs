using System.Collections;
using System.Collections.Generic;
using Entity.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Enemy {
    public class BossProjectile : MonoBehaviour {
        // OutLet
        private Rigidbody2D _rigidbody2D;
        private Transform target;
        [SerializeField] private float shotSpeed;
        public float attackDamage = 2;
        [SerializeField] private float maxLife = 2.0f;
        public bool damageable = true;
        public int timer = 25;
        private float lifeBtwTimer;
        public GameObject destoryEffect;
        public float health;

        private void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _rigidbody2D.velocity = new Vector2(-shotSpeed/2f, 0);
            health = maxLife;
        }

        void Update() {
            float acceleration = shotSpeed / 80f;
            float maxSpeed = shotSpeed;

            // Home in on target
            Vector2 directionToTarget = target.position - transform.position;
            _rigidbody2D.MoveRotation(Mathf.Atan2(-_rigidbody2D.velocity.y, -_rigidbody2D.velocity.x)* Mathf.Rad2Deg);
            // Accelerate
            _rigidbody2D.AddForce(directionToTarget * acceleration);

            // Cap max speed
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, maxSpeed);
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
                //Instantiate(destoryEffect, transform.position, Quaternion.identity);

                collision.gameObject.SendMessage("OnDamage", attackDamage);

                Destroy(gameObject);

                GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("ArmAttackFinished");
            }
        }

        public void OnDamage(float dmg) {
            if (damageable) {
                health = Mathf.Max(health - dmg, 0);
                damageable = false;
                if (health <= 0) {
                    Destroy(gameObject);

                    GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("ArmAttackFinished");
                }
            }
        }
    }
}
