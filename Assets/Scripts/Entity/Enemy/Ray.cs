using System.Collections;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Enemy {
    public class Ray : Entity {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxDist;
        public Transform firePoint;
        private Transform _target;
        public float attackDamage = 1;
        [SerializeField] private Transform targetA, targetB;

        private LineRenderer _lineRenderer;
        [SerializeField] private Gradient redColor, greenColor;

        // private SpriteRenderer spriteRenderer;
        // [SerializeField] private bool isLeft;
        private int _indexNum;

        private new void Start() {
            base.Start();
            Physics2D.queriesStartInColliders = false;
            _target = targetB;
            _lineRenderer = GetComponentInChildren<LineRenderer>();
            
            // spriteRenderer = GetComponent<SpriteRenderer>();

            if (!FacingRight) {
                SpriteRenderer.flipX = true;
                _indexNum = -1;
            }
            else {
                _indexNum = 1;
            }
        }

        private new void Update() {
            base.Update();
            Move();
            Detect();
        }

        private void Move() {
            if (Vector2.Distance(transform.position, targetA.position) <= 0.1f) {
                _target = targetB;
            }

            if (Vector2.Distance(transform.position, targetB.position) <= 0.1f) {
                _target = targetA;
            }

            transform.position = Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
        }

        private void SinMove() {
            transform.position = new Vector2(transform.position.x, (Mathf.Sin(Time.time * moveSpeed) * 4.5f) + 1.5f);
        }

        private void Detect() {
            RaycastHit2D hitInfo;

            hitInfo = Physics2D.Raycast(firePoint.position, _indexNum * transform.right, maxDist);

            if (hitInfo.collider != null) {
                if (hitInfo.collider.tag == "Block") {
                    Debug.DrawLine(firePoint.position, hitInfo.point, Color.green);

                    _lineRenderer.SetPosition(1, hitInfo.point);
                    _lineRenderer.colorGradient = greenColor;
                }
                else if (hitInfo.collider.CompareTag("Player")) {
                    Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);

                    _lineRenderer.SetPosition(1, hitInfo.point);
                    _lineRenderer.colorGradient = redColor;
                    // cur off health;
                    hitInfo.collider.gameObject.SendMessage("OnDamage", attackDamage);
                }
            }
            else {
                int temp = _indexNum * 2;
                _lineRenderer.SetPosition(1,
                    new Vector2(firePoint.transform.position.x + temp, firePoint.transform.position.y));
                _lineRenderer.colorGradient = greenColor;
            }

            _lineRenderer.SetPosition(0, firePoint.transform.position);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player")) {
                collision.gameObject.SendMessage("OnDamage", attackDamage);
            }

            if (_target == targetA) {
                _target = targetB;
            }
            else {
                _target = targetA;
            }
        }
    }
}