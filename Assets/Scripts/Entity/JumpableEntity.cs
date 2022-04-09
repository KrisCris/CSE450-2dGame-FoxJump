using System;
using System.Linq;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        public int maxJumps = 1;
        public float raycastOffset = 0f;
        protected int CurrJumps;

        private bool _isGrounded = true;
        private bool _touchingGround = true;


        protected new void Start() {
            base.Start();
            CurrJumps = maxJumps;
        }

        protected new void Update() {
            base.Update();
            _isGrounded = CurrJumps == maxJumps && _touchingGround;
            // Animator.SetFloat("VerticalSpeed", Rigidbody2D.velocity.y);
            // _isGrounded = CurrJumps == maxJumps;
            if (_isGrounded) {
                Animator.SetBool("IsGrounded", _isGrounded);
            } else {
                Animator.SetBool("IsGrounded", _isGrounded);
                Animator.SetBool("IsJumping", Rigidbody2D.velocity.y > 0);
                Animator.SetBool("IsFalling", Rigidbody2D.velocity.y < 0);
            }
        }

        protected void PerformJump() {
            if (CurrJumps > 0) {
                --CurrJumps;
                Rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                _touchingGround = false;
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
                        _touchingGround = true;
                    }
                }
            }
        }
    }
}