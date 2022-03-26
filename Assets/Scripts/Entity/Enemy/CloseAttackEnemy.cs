using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Enemy {
    public class CloseAttackEnemy : Entity {
        [SerializeField] private float moveSpeed;
        private Transform _target;
        public float attackDamage = 2;
        [SerializeField] private Transform targetA, targetB;

        // private SpriteRenderer _spriteRenderer;

        // [SerializeField] private bool isLeft;
        protected new void Start() {
            base.Start();
            _target = targetB;
            // _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
        protected new void Update() {
            base.Update();
            Move();
        }
    
        private void Move() {
            if (Vector2.Distance(transform.position, targetA.position) <= 0.1f) {
                _target = targetB;
                SpriteRenderer.flipX = false;
                
            }
    
            if (Vector2.Distance(transform.position, targetB.position) <= 0.1f) {
                _target = targetA;
                SpriteRenderer.flipX = true;
    
            }
    
            transform.position = Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
        }
    
        private void OnCollisionStay2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player")) {
                collision.gameObject.SendMessage("OnDamage", attackDamage);
            }
    
            if (_target == targetA) {
                _target = targetB;
                SpriteRenderer.flipX = false;
            }
            else {
                _target = targetA;
                SpriteRenderer.flipX = true;

            }
        }
    }
}
