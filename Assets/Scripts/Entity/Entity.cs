using UnityEngine;

namespace Entity {
    public class Entity : MonoBehaviour {
        protected Rigidbody2D Rigidbody2D;
        protected Collider2D Collider2D;
        protected SpriteRenderer SpriteRenderer;
        protected Animator Animator;

        public float maxHealth = 10;
        public bool damageable = true;

        private float _health;
        protected bool FacingRight;
        
        private void InitProperties() {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            _health = maxHealth;
            FacingRight = true;
        }

        protected void Start() {
            InitProperties();
        }

        protected void Update() {
            // TODO Entity  Movement? 
            // TODO health management?
            Debug.Log("[HEALTH] "+_health);
        }

        private bool OnDamage<T>(float dmg, T source) {
            if (damageable) {
                _health = Mathf.Max(_health - dmg, 0);
                _health = _health <= dmg ? 0 : _health - dmg;
                if (_health <= 0) {
                    OnDeath("Killed by Game Design.");
                }
            }

            return damageable;
        }

        private float OnHealing<T>(float heal, T source) {
            _health = Mathf.Min(heal + _health, maxHealth);
            return _health;
        }

        private void OnDeath(string reason) {
            // TODO Die
            print("died for "+reason);
        }
        
        protected void FlipFacing() {
            SpriteRenderer.flipX = FacingRight;
            FacingRight = !FacingRight;
        }
    }
}