using Entity.Enemy;
using Item;
using UnityEngine;

namespace Entity.Player {
    public class DefaultProjectile : MonoBehaviour {
        private Rigidbody2D _rb;
        private PlayerEntity _player;
        public GameObject destroyVFX;

        private float _creationTime;

        public void Init(Vector3 pos, float speed, bool isRight, PlayerEntity player) {
            _creationTime = Time.time;
            if (!_rb) {
                _rb = GetComponent<Rigidbody2D>();
            }

            if (!_player) {
                _player = player;
            }

            transform.position = pos;
            _rb.velocity = transform.right * speed * (isRight ? 1f : -1f);
        }

        // Self-Destroy after 30 seconds.
        private void Update() {
            if (Time.time - _creationTime > 30) {
                // No hit enemy, return the projectile
                // _player.OnItemCollect(Items.Projectile, 1);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.collider.CompareTag("Enemy")) {
                if (col.gameObject.GetComponentInChildren<HeadKill>()) {
                    col.gameObject.GetComponentInChildren<HeadKill>().KillMob();
                } else {
                    Destroy(col.gameObject.transform.parent.gameObject);
                }
            } else if (col.collider.CompareTag("Enemy2")) {
                if (col.gameObject.GetComponentInChildren<HeadKill>()) {
                    col.gameObject.GetComponentInChildren<HeadKill>().KillMob();
                } else {
                    Destroy(col.gameObject);
                }
            } else if (col.collider.CompareTag("Boss")) {
                col.gameObject.SendMessage("OnDamage", 1f);
            } else {
                // No hit enemy, return the projectile
                // _player.OnItemCollect(Items.Projectile, 1);
            }

            if (destroyVFX) {
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void OnBecameInvisible() {
            // No hit enemy, return the projectile
            // _player.OnItemCollect(Items.Projectile, 1);
            Destroy(gameObject);
        }
    }
}