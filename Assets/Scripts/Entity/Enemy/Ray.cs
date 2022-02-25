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
        private Transform target;
        public float attack_value;
        [SerializeField] private Transform targetA, targetB;

        private LineRenderer lineRenderer;
        [SerializeField] private Gradient redColor, greenColor;

        // private SpriteRenderer spriteRenderer;
        // [SerializeField] private bool isLeft;
        private int indexNum;

        private new void Start() {
            base.Start();
            Physics2D.queriesStartInColliders = false;
            target = targetB;
            lineRenderer = GetComponentInChildren<LineRenderer>();
            
            // spriteRenderer = GetComponent<SpriteRenderer>();

            if (!FacingRight) {
                SpriteRenderer.flipX = true;
                indexNum = -1;
            }
            else {
                indexNum = 1;
            }
        }

        private new void Update() {
            base.Update();
            Move();
            Detect();
        }

        private void Move() {
            if (Vector2.Distance(transform.position, targetA.position) <= 0.1f) {
                target = targetB;
            }

            if (Vector2.Distance(transform.position, targetB.position) <= 0.1f) {
                target = targetA;
            }

            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        private void SinMove() {
            transform.position = new Vector2(transform.position.x, (Mathf.Sin(Time.time * moveSpeed) * 4.5f) + 1.5f);
        }

        private void Detect() {
            RaycastHit2D hitInfo;

            hitInfo = Physics2D.Raycast(firePoint.position, indexNum * transform.right, maxDist);

            if (hitInfo.collider != null) {
                if (hitInfo.collider.tag == "Block") {
                    Debug.DrawLine(firePoint.position, hitInfo.point, Color.green);

                    lineRenderer.SetPosition(1, hitInfo.point);
                    lineRenderer.colorGradient = greenColor;
                }
                else if (hitInfo.collider.CompareTag("Player")) {
                    Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);

                    lineRenderer.SetPosition(1, hitInfo.point);
                    lineRenderer.colorGradient = redColor;
                    // cur off health;
                    hitInfo.collider.gameObject.SendMessage("OnDamage", attack_value);
                }
            }
            else {
                int temp = indexNum * 2;
                lineRenderer.SetPosition(1,
                    new Vector2(firePoint.transform.position.x + temp, firePoint.transform.position.y));
                lineRenderer.colorGradient = greenColor;
            }

            lineRenderer.SetPosition(0, firePoint.transform.position);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (collision.collider.CompareTag("Player")) {
                collision.gameObject.SendMessage("OnDamage", attack_value);
            }

            if (target == targetA) {
                target = targetB;
            }
            else {
                target = targetA;
            }
        }
    }
}