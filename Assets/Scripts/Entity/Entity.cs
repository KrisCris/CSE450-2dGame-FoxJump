using Entity.Player;
using UnityEngine;
using UI;
using UnityEngine.SceneManagement;

namespace Entity {
    public class Entity : MonoBehaviour {
        protected Rigidbody2D Rigidbody2D;
        protected Collider2D Collider2D;
        protected SpriteRenderer SpriteRenderer;
        protected Animator Animator;

        public float maxHealth = 10;
        public bool damageable = true;
        public int timer = 25;

        public GameObject healthBar;
        private float _health;
        protected bool FacingRight;
        
        private void InitProperties() {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            _health = maxHealth;
            FacingRight = true;
            SpriteRenderer.flipX = !FacingRight;
            if (healthBar) {
                healthBar.GetComponent<HealthBar>().Init(maxHealth);
            }
        }

        protected void Start() {
            InitProperties();
        }
        

        protected void FixedUpdate() {
            if (timer == 0) {
                damageable = true;
                timer = 25;
            }
            if (!damageable && timer > 0) {
                --timer;
            }
        }

        protected void Update() {
            // TODO Entity  Movement? 
            // TODO health management?
            
        }

        private void OnDamage(float dmg) {
            if (damageable) {
                _health = Mathf.Max(_health - dmg, 0);
                damageable = false;
                if (healthBar) {
                    healthBar.GetComponent<HealthBar>().UpdateHealthBar(_health, maxHealth);
                }
                if (_health <= 0) {
                    OnDeath("Killed by Game Design.");
                }
            }
            
        }

        private float OnHealing(float heal) {
            _health = Mathf.Min(heal + _health, maxHealth);
            if (healthBar) {
                healthBar.GetComponent<HealthBar>().UpdateHealthBar(_health, maxHealth);
            }
            return _health;
        }

        private void OnDeath(string reason) {
            // TODO Die
            print("died for "+reason);
            if (!GetComponent<PlayerEntity>()) {
                Destroy(gameObject);
            } else {
                OnReborn();
            }
            
        }

        private void OnReborn() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        protected void FlipFacing() {
            SpriteRenderer.flipX = FacingRight;
            FacingRight = !FacingRight;
        }
    }
}