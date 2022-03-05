using System;
using UnityEngine;

namespace Entity.Player {
    public class DefaultProjectile : MonoBehaviour {
        private Rigidbody2D _rb;
        private float _creationTime;

        // Start is called before the first frame update
        private void Start() {
            _creationTime = Time.time;
            if (!_rb) {
                _rb = GetComponent<Rigidbody2D>();
            }
            _rb.velocity = transform.right * 10f;
        }

        // Self-Destroy after 30 seconds.
        private void Update() {
            if (Time.time - _creationTime > 30) {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.collider.CompareTag("Enemy")) {
                Destroy(col.gameObject.transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}