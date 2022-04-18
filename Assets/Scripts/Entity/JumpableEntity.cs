using System;
using Entity.Player;
using UnityEngine;

namespace Entity {
    public class JumpableEntity : Entity {
        [Header("State")] public float jumpForce = 10f;
        public int maxJumps = 1;
        public int currJumps;

        protected bool IsGrounded = true;
        public int jumpIFrames = 4;
        public int jumpIFrameCountdown = 0;
        public bool jumpProtection = false;


        protected new void Start() {
            base.Start();
            currJumps = maxJumps;
        }

        protected new void Update() {
            base.Update();
            IsGrounded = currJumps == maxJumps && DetectGround();

            Animator.SetBool("IsGrounded", IsGrounded);
            Animator.SetBool("IsJumping", Rigidbody2D.velocity.y > 0.3 && (currJumps < maxJumps));
            Animator.SetBool("IsFalling", Rigidbody2D.velocity.y < -0.3);
        }

        protected new void FixedUpdate() {
            base.FixedUpdate();

            // Jump Invincible
            if (GetComponent<PlayerEntity>()) {
                if (jumpIFrameCountdown > 0) {
                    damageable = false;
                    jumpProtection = true;
                    --jumpIFrameCountdown;
                } else if (jumpProtection) {
                    damageable = true;
                    jumpProtection = false;
                }
            }
        }

        protected virtual bool PerformJump(bool free = false, float directionX = 0f, float directionY = 1f, int freeReward = 0) {
            if (currJumps > 0 || free) {
                if (!free) --currJumps;
                Rigidbody2D.AddForce(new Vector2(directionX, directionY) * jumpForce, ForceMode2D.Impulse);
                jumpIFrameCountdown = jumpIFrames;
                if (free) {
                    Animator.SetBool("IsJumping", true);
                    currJumps = Mathf.Max(currJumps + freeReward, maxJumps);
                }
                return true;
            }
            return false;
        }

        public void UpdateMaxJump(int offset) {
            maxJumps = Math.Max(maxJumps + offset, 0);
            currJumps = Math.Max(currJumps + offset, 0);
            GameController.Instance.maxJumps += offset;
        }

        public void SetJumps(int j) {
            maxJumps = j;
            currJumps = j;
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && DetectGround()) {
                currJumps = maxJumps;
            }
        }

        private bool DetectGround() {
            if (GetComponent<PlayerEntity>()) {
                PlayerEntity that = (PlayerEntity) this;
                RaycastHit2D ptBack =
                    RayCastHelper.RayCast(that.GetBackFoot(), Vector2.down, that.checkDistance, "Ground");
                RaycastHit2D ptFront =
                    RayCastHelper.RayCast(that.GetFrontFoot(), Vector2.down, that.checkDistance, "Ground");
                if (ptBack || ptFront) {
                    return true;
                }
            } else {
                RaycastHit2D pt = RayCastHelper.RayCast(transform.position, -transform.up, .8f, "Ground");
                if (pt) {
                    return true;
                }
            }

            return false;
            // RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(raycastOffset, 0f, 0f), -transform.up, .8f);
            // hits = hits.Concat(Physics2D.RaycastAll(transform.position + new Vector3(-raycastOffset, 0f, 0f),
            //     -transform.up, .8f)).ToArray();
            // foreach (RaycastHit2D hit in hits) {
            //     if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            //         return true;
            //     }
            // }
            // return false;
        }

        // private void OnCollisionExit2D(Collision2D other) {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        //         TouchingGround = false;
        //     }
        // }
    }
}