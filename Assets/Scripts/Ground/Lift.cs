using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ground {
    public class Lift : MonoBehaviour
    {
        // Outlet
        public Transform upCheckPoint;
        public Transform downCheckPoint;
        public float speed;
        
        public float upBound;
        public float downBound;

        private bool _isUp;
        private Vector3 _position;
        private Vector3 _shift;
        
        // Start is called before the first frame update
        void Start() {
            _isUp = true;
            _position = transform.position;
            _shift = new Vector3(0f, 0f, 0f);
            upBound = upCheckPoint.position.y;
            downBound = downCheckPoint.position.y;
        }

        // Update is called once per frame
        void Update() {
            if (transform.position.y > upBound || transform.position.y < downBound) {
                _isUp = !_isUp;
            }
            _shift += (_isUp ? 1f : -1f) * transform.up * speed * 0.001f;
            transform.position = _position + _shift;
        }
    }
}
