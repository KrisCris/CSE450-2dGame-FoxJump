using System;
using System.Linq;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        public int maxJumps = 1;
        public float raycastOffset = 0f;
        protected int CurrJumps;

        private bool _isGrounded = true;


        protected new void Start() {
            base.Start();
            CurrJumps = maxJumps;
        }

        protected new void Update() {
            base.Update();
            _isGrounded = CurrJumps == maxJumps && Mathf.Abs(Rigidbody2D.velocity.y) <= 0.5;
            if (_isGrounded) {
                Animator.SetBool("IsGrounded", _isGrounded);
            } else {
                Animator.SetBool("IsGrounded", _isGrounded);
                Animator.SetBool("IsJumping", Rigidbody2D.velocity.y > 0);
                Animator.SetBool("IsFalling", Rigidbody2D.velocity.y < 0);
            }

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