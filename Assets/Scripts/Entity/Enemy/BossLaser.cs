using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Enemy {
    public class BossLaser : MonoBehaviour {
        public Animator animator;
        private Rigidbody2D _rigidbody2D;
        private Transform target;
        private Collider2D _collider2D;
        public float attackDamage;

        // Start is called before the first frame update
        void Start() {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            Vector2 directionToTarget = - target.position + transform.position;
            _rigidbody2D.MoveRotation(Mathf.Atan2(directionToTarget.y, directionToTarget.x)* Mathf.Rad2Deg);
            StartCoroutine("AttackFinished");
        }

        // Update is called once per frame
        void Update() { }

        IEnumerator AttackFinished() {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
            GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("LaserAttackFinished");
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player"))
            {
                //Instantiate(destoryEffect, transform.position, Quaternion.identity);

                collision.gameObject.SendMessage("OnDamage", attackDamage);
                GameObject.FindGameObjectWithTag("Boss").gameObject.SendMessage("LaserAttackFinished");

                Destroy(gameObject);
            }
        }
        
        
        public void OnDamage(float dmg) {
        }
    }
}
