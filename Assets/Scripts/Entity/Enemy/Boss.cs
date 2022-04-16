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
        public float attackFrequent;
        public Transform armPosition;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Finished = Animator.StringToHash("AttackFinished");

        // State Track
        public int attackType = 0;
        private bool _isDead = false;

        protected void Start() {
            base.Start();
            StartCoroutine("AttackTimer", 1);
        }

        IEnumerator AttackTimer() {
            yield return new WaitForSeconds(attackFrequent);
            attackType += 1;

            if (attackType % 1 == 0 && !_isDead) {
                Animator.SetBool(Attacking, true);
                yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length - 0.18f);
                ArmAttack();
            }
        }

        void ArmAttack() {
            Instantiate(arm, armPosition.position, Quaternion.identity);
        }

        public void AttackFinished() {
            Animator.SetBool(Attacking, false);
            Animator.SetTrigger(Finished);
            StartCoroutine("AttackTimer");
        }

        protected override void OnDeath(string reason) {
            Animator.SetTrigger(Dead);
            Collider2D.enabled = false;
            _isDead = true;
        }
    }
    
}
