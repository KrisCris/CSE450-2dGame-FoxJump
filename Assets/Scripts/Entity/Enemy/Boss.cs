using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Player;
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
        
        // modifier
        public bool safeLock = true;
        public int touchDamage = 2;

        public void StartAttack() {
            arm.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(nameof(ArmAttackTimer));
            StartCoroutine(nameof(LaserAttackTimer));
        }

        IEnumerator LaserAttackTimer() {
            yield return new WaitForSeconds(laserAttackFrequent);

            if (!_isDead) {
                LaserAttack();
            }

            if (!safeLock) {
                yield return LaserAttackTimer();
            }
        }

        IEnumerator ArmAttackTimer() {
            yield return new WaitForSeconds(armAttackFrequent);

            if (!_isDead) {
                Animator.SetBool(Attacking, true);
                yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
                ArmAttack();
                if (!safeLock) {
                    yield return ArmAttackTimer();
                }
            }
        }

        void ArmAttack() {
            _currentArmAttack = Instantiate(arm, armPosition.position, Quaternion.identity);
        }

        void LaserAttack() {
            _currentLaserAttack = Instantiate(laser, transform.position, Quaternion.identity);
        }

        public void ArmAttackFinished() {
            if (safeLock) {
                Animator.SetBool(Attacking, false);
                Animator.SetTrigger(Finished);
                StartCoroutine(nameof(ArmAttackTimer));
            }
        }

        public void LaserAttackFinished() {
            if (safeLock) {
                StartCoroutine(nameof(LaserAttackTimer));
            }
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

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.GetComponent<PlayerEntity>()) {
                col.gameObject.GetComponent<PlayerEntity>().SendMessage("OnDamage", touchDamage);
            }
        }
    }
    
}
