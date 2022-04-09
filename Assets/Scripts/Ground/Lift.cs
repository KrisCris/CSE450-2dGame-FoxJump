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
        public float speed = 1;
        public bool isTriggered = true;
        
        public float upBound;
        public float downBound;

        private bool _isUp;
        private Vector3 _position;
        private Vector3 _shift;
        private GameObject _ground;
        
        // Start is called before the first frame update
        void Start() {
            _isUp = true;
            _ground = gameObject.transform.Find("LiftGround").gameObject;
            _position = _ground.transform.position;
            _shift = new Vector3(0f, 0f, 0f);
            upBound = upCheckPoint.position.y;
            downBound = downCheckPoint.position.y;
        }

        // Update is called once per frame
        void Update() {
            if (isTriggered) {
                if (_ground.transform.position.y > upBound || _ground.transform.position.y < downBound) {
                    _isUp = !_isUp;
                }

                _shift += (_isUp ? 1f : -1f) * _ground.transform.up * speed * Time.fixedTime * 0.0008f;
                _ground.transform.position = _position + _shift;
            }
        }

        void Triggered() {
            gameObject.transform.Find("LiftGround").gameObject.SetActive(true);
            isTriggered = true;
        }
    }
}
