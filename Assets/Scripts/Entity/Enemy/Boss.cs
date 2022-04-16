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
        public float armAttackFrequent;
        public float laserAttackFrequent;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Finished = Animator.StringToHash("AttackFinished");

        // State Track
        private bool _isDead = false;
        private GameObject _currentArmAttack;
        private GameObject _currentLaserAttack;

        void StartAttack() {
            StartCoroutine("ArmAttackTimer");
            StartCoroutine("LaserAttackTimer");
        }

        IEnumerator LaserAttackTimer() {
            yield return new WaitForSeconds(laserAttackFrequent);

            if (!_isDead) {
                LaserAttack();
            }
        }

        IEnumerator ArmAttackTimer() {
            yield return new WaitForSeconds(armAttackFrequent);

            if (!_isDead) {
                Animator.SetBool(Attacking, true);
                yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
                ArmAttack();
            }
        }

        void ArmAttack() {
            _currentArmAttack = Instantiate(arm, armPosition.position, Quaternion.identity);
        }

        void LaserAttack() {
            _currentLaserAttack = Instantiate(laser, transform.position, Quaternion.identity);
        }

        public void ArmAttackFinished() {
            Animator.SetBool(Attacking, false);
            Animator.SetTrigger(Finished);
            StartCoroutine("ArmAttackTimer");
        }

        public void LaserAttackFinished() {
            StartCoroutine("LaserAttackTimer");
        }

        protected override void OnDeath(string reason) {
            if (_currentLaserAttack) {
                Destroy(_currentLaserAttack);
            }
            if (_currentArmAttack) {
                Destroy(_currentArmAttack);
            }
            Animator.SetTrigger(Dead);
            Collider2D.enabled = false;
            _isDead = true;
        }
    }
    
}
