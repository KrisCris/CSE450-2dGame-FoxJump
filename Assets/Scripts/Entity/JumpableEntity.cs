using System;
using System.Linq;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        public int maxJumps = 1;
        public float raycastOffset = 0f;
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
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(raycastOffset, 0f, 0f), -transform.up, 0.7f);
                hits = hits.Concat(Physics2D.RaycastAll(transform.position + new Vector3(-raycastOffset, 0f, 0f),
                    -transform.up, 0.7f)).ToArray();
                foreach (RaycastHit2D hit in hits) {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                        CurrJumps = maxJumps;
                    }
                }
            }
        }
    }
}