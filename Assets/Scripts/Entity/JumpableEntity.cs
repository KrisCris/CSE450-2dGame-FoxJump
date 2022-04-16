using System;
using System.Linq;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        [Header("State")] 
        public float jumpForce = 10f;
        public int maxJumps = 1;
        public float raycastOffset = 0f;
        [SerializeField]
        protected int CurrJumps;

        protected bool IsGrounded = true;
        // protected bool TouchingGround = true;


        protected new void Start() {
            base.Start();
            CurrJumps = maxJumps;
        }

        protected new void Update() {
            base.Update();
            IsGrounded = CurrJumps == maxJumps && DetectGround();
            // Animator.SetFloat("VerticalSpeed", Rigidbody2D.velocity.y);
            // _isGrounded = CurrJumps == maxJumps;
            // if (IsGrounded) {
            //     Animator.SetBool("IsGrounded", IsGrounded);
            // } else {
                Animator.SetBool("IsGrounded", IsGrounded);
                Animator.SetBool("IsJumping", Rigidbody2D.velocity.y > 0.3 && CurrJumps < maxJumps);
                Animator.SetBool("IsFalling", Rigidbody2D.velocity.y < -0.3);
            // }
        }

        protected bool PerformJump() {
            if (CurrJumps > 0) {
                --CurrJumps;
                Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // TouchingGround = false;
                return true;
            }

            return false;
        }

        public void UpdateMaxJump(int offset) {
            maxJumps = Math.Max(maxJumps + offset, 0);
            CurrJumps = Math.Max(CurrJumps + offset, 0);
        }

        public void SetJumps(int j) {
            maxJumps = j;
            CurrJumps = j;
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && DetectGround()) {
                CurrJumps = maxJumps;
            }
        }

        private bool DetectGround() {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(raycastOffset, 0f, 0f), -transform.up, .8f);
            hits = hits.Concat(Physics2D.RaycastAll(transform.position + new Vector3(-raycastOffset, 0f, 0f),
                -transform.up, .8f)).ToArray();
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    return true;
                }
            }
            return false;
        }

        // private void OnCollisionExit2D(Collision2D other) {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        //         TouchingGround = false;
        //     }
        // }
    }
}