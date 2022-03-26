using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entity.Player {
    public class DefaultProjectile : MonoBehaviour {
        private Rigidbody2D _rb;
        private float _creationTime;
        // private static Object _prefab = Resources.Load("Prefabs/DefaultProjectile.prefab");

        // Start is called before the first frame update
        private void Start() {

        }

        public void Init(Vector3 pos, float speed, bool isRight) {
            _creationTime = Time.time;
            if (!_rb) {
                _rb = GetComponent<Rigidbody2D>();
            }
            transform.position = pos;
            _rb.velocity = transform.right * speed * (isRight ? 1f : -1f);
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