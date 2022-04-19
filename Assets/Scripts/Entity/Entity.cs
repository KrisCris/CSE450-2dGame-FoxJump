using Entity.Player;
using UnityEngine;
using UI;
using UnityEngine.SceneManagement;

namespace Entity {
    public class Entity : MonoBehaviour {
        [Header("Outlets")] protected Rigidbody2D Rigidbody2D;
        public Collider2D Collider2D;
        protected SpriteRenderer SpriteRenderer;
        public Animator Animator;
        public GameObject healthBar;

        [Header("State")] public float maxHealth = 10;
        public bool damageable = true;
        public int dmgIFrames = 30;
        public int dmgIFrameCountdown = 0;
        public bool dmgProtection = false;
        public float speed = 12;
        public float knockBackForce = 1;

        public float health;
        public bool FacingRight = true;

        private void InitProperties() {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            if (!Collider2D) {
                Collider2D = GetComponent<Collider2D>();
            }

            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            health = maxHealth;
            SpriteRenderer.flipX = !FacingRight;
            if (healthBar) {
                healthBar.GetComponent<HealthBar>().Init(maxHealth);
            }
        }

        protected void Start() {
            InitProperties();
        }

        protected void FixedUpdate() {
            if (dmgIFrameCountdown > 0) {
                dmgProtection = true;
                --dmgIFrameCountdown;
            } else if (dmgProtection) {
                if (GetComponent<PlayerEntity>()) {
                    Animator.SetBool("IsDamaging", false);
                }

                dmgProtection = false;
                damageable = true;
            }
        }

        protected void Update() {
            // TODO Entity  Movement? 
            // TODO health management?            
        }

        public void SetAnimationSpeed(float speed) {
            Animator.speed = speed;
        }

        public void SetColliderState(bool state) {
            Collider2D.enabled = state;
        }

        protected void Move(Vector2 direction, float speed) {
            if (FacingRight == direction.x < 0) {
                FlipFacing();
            }
            
            Vector2 slopeDirection = GetSlopeDirection();
            if (Mathf.Abs(slopeDirection.y) > 0 && Mathf.Abs(slopeDirection.y) < 1) {
                RayCastHelper.RayCast(transform.position,slopeDirection, .5f, "Ground");
                direction = slopeDirection;
                speed /= Mathf.Abs(1 - slopeDirection.y);
            }
            
            Rigidbody2D.AddForce(direction * (speed * Time.deltaTime), ForceMode2D.Impulse);
        }

        protected void Move(Vector2 direction) {
            Move(direction, speed);
        }

        public int GetFaceDirection() {
            return FacingRight ? 1 : -1;
        }

        public float GetX() {
            return Rigidbody2D.transform.position.x;
        }

        public float GetY() {
            return Rigidbody2D.transform.position.y;
        }

        private void OnDamage(float dmg) {
            if (damageable && health > 0) {
                if (GetComponent<PlayerEntity>()) {
                    KnockBack();
                }

                health = Mathf.Max(health - dmg, 0);
                damageable = false;
                dmgIFrameCountdown = dmgIFrames;
                if (healthBar) {
                    healthBar.GetComponent<HealthBar>().UpdateHealthBar(health, maxHealth);
                }

                if (GetComponent<PlayerEntity>()) {
                    Animator.SetBool("IsDamaging", true);
                }

                if (health <= 0) {
                    OnDeath("Killed by Game Design.");
                }
            }
        }

        public void KnockBack() {
            Rigidbody2D.AddForce(new Vector2(0, 10f) * knockBackForce, ForceMode2D.Impulse);
        }

        public void UpdatePos(Vector3 pos) {
            gameObject.transform.position = pos;
        }

        private float OnHealing(float heal) {
            health = Mathf.Min(heal + health, maxHealth);
            if (healthBar) {
                healthBar.GetComponent<HealthBar>().UpdateHealthBar(health, maxHealth);
            }

            return health;
        }

        protected virtual void OnDeath(string reason) {
            Destroy(gameObject);
            // TODO Die
            // print("died for "+reason);
            // if (!GetComponent<PlayerEntity>()) {
            //     Destroy(gameObject);
            // } else {
            //     OnReborn();
            // }            
        }

        // private void OnReborn() {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
        //     SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        // }

        protected void FlipFacing() {
            if (!Rigidbody2D) return;
            SpriteRenderer.flipX = FacingRight;
            FacingRight = !FacingRight;
        }

        public float entityRayDist = .4f;
        public float entityFootYOffset = -.2f;
        public float ftOffset0 = .5f;
        public float ftOffset1 = .7f;

        private Vector2 GetSlopeDirection() {
            Vector2 pt1 = new Vector2(GetX() + ftOffset0 * GetFaceDirection(), GetY() + entityFootYOffset);
            Vector2 pt2 = new Vector2(GetX() + ftOffset1 * GetFaceDirection(), GetY() + entityFootYOffset);
            Vector2 groundDirection = new Vector2(GetFaceDirection(), 0);
            RaycastHit2D ptFore = RayCastHelper.RayCast(pt2, Vector2.down, entityRayDist, "Ground");
            RaycastHit2D ptBehind = RayCastHelper.RayCast(pt1, Vector2.down, entityRayDist, "Ground");

            if (ptBehind && ptFore) {
                groundDirection = ptFore.point - ptBehind.point;
                groundDirection = groundDirection.normalized;
                if (Mathf.Abs(groundDirection.y) < 1E-3) {
                    groundDirection.y = 0;
                }
            }

            Debug.Log(groundDirection);
            return groundDirection;
        }
    }
}