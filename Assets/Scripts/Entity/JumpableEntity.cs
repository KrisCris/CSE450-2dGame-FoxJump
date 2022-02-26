using System;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        public int maxJumps = 1;
        protected int CurrJumps;

        private bool passiveJumping;

        protected new void Start() {
            base.Start();
            CurrJumps = maxJumps;
            passiveJumping = false;
        }

        protected new void Update() {
            base.Update();
            Animator.SetBool("IsJumping", CurrJumps < maxJumps || passiveJumping);
        }

        public void UpdateMaxJump(int offset) {
            maxJumps = Math.Max(maxJumps + offset, 0);
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                // to check we are indeed hitting ground other than top or side of Ground layer objects
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.7f);
                foreach (RaycastHit2D hit in hits) {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                        CurrJumps = maxJumps;
                    }
                }
            }
        }
    }
}