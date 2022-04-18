using UnityEngine;

namespace Ground {
    public class Grass : MonoBehaviour {
        private Animator _animator;
        
        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            _animator.SetTrigger("Touching");
        }
    }
}