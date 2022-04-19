using System;
using System.Collections;
using UnityEngine;

namespace Entity.Enemy {
    public class BossLaser : MonoBehaviour {
        public AudioSource laserSound;
        public float attackDamage;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;

        private Transform _target;
        private bool _discharged;
        public GameObject collisionVFX;

        void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _discharged = false;

            if (GameObject.FindGameObjectWithTag("Player")) {
                _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                Vector2 directionToTarget = -_target.position + transform.position;
                _rigidbody2D.MoveRotation(Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg);
                StartCoroutine(nameof(AttackFinished));
            }
        }

        void LateUpdate() {
            if (String.Equals(_spriteRenderer.sprite.name, "Laser_sheet_29")) {
                if (laserSound && !laserSound.isPlaying && !_discharged) {
                    laserSound.Play();
                    _discharged = true;
                }
            }
        }

        private IEnumerator AttackFinished() {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
            GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("LaserAttackFinished");
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player")) {
                if (collisionVFX) {
                    Instantiate(collisionVFX, collision.gameObject.transform.position, Quaternion.identity);
                }

                collision.gameObject.SendMessage("OnDamage", attackDamage);
                GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("LaserAttackFinished");

                Destroy(gameObject);
            }
        }
        public void OnDamage(float dmg) { }
    }
}