using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entity.Enemy {
    public class Boss : Entity
    {
        // Outlet
        public GameObject arm;
        public Transform armPosition;
        public GameObject laser;
        public float attackFrequent;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Finished = Animator.StringToHash("AttackFinished");

        // State Track
        public int attackType = 0;
        private bool _isDead = false;
        private GameObject _currentAttack;

        protected void Start() {
            base.Start();
            StartCoroutine("AttackTimer", 1);
        }

        IEnumerator AttackTimer() {
            yield return new WaitForSeconds(attackFrequent);
            attackType += 1;

            if (!_isDead) {
                if (attackType % 4 == 0) {
                    Animator.SetBool(Attacking, true);
                    yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
                    ArmAttack();
                }
                else {
                    LaserAttack();
                }
            }
        }

        void ArmAttack() {
            _currentAttack = Instantiate(arm, armPosition.position, Quaternion.identity);
        }

        void LaserAttack() {
            _currentAttack = Instantiate(laser, transform.position, Quaternion.identity);
        }

        public void AttackFinished() {
            Animator.SetBool(Attacking, false);
            Animator.SetTrigger(Finished);
            StartCoroutine("AttackTimer");
        }

        protected override void OnDeath(string reason) {
            if (_currentAttack) {
                Destroy(_currentAttack);
            }
            Animator.SetTrigger(Dead);
            Collider2D.enabled = false;
            _isDead = true;
        }
    }
    
}
