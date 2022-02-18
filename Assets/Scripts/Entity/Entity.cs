using System;
using UnityEngine;
using TMPro;

namespace Entity {
    public class Entity : MonoBehaviour {
        protected Rigidbody2D Rigidbody2D;
        protected Collider2D Collider2D;
        protected SpriteRenderer SpriteRenderer;
        protected Animator Animator;

        public float maxHealth = 10;
        public bool damageable = true;
        public int timer = 25;

        public TextMeshProUGUI healthText;
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

        protected void FixedUpdate() {
            if (timer == 0) {
                damageable = true;
                timer = 25;
            }
            if (!damageable && timer > 0) {
                --timer;
            }
            print("damageable" + damageable);
            print("timer" + timer);
            // Debug.Log("[HEALTH] "+_health);
        }

        protected void Update() {
            // TODO Entity  Movement? 
            // TODO health management?
            
        }

        private void OnDamage(float dmg) {
            if (damageable) {
                _health = Mathf.Max(_health - dmg, 0);
                damageable = false;
                if (_health <= 0) {
                    OnDeath("Killed by Game Design.");
                }
            }
            healthText.text = _health + "";
        }

        private float OnHealing<T>(float heal, T source) {
            _health = Mathf.Min(heal + _health, maxHealth);
            healthText.text = _health + "";
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