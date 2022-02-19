using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_attck : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    private Transform target;
    public float attack_value;
    [SerializeField] private Transform targetA, targetB;
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private bool isLeft;

    // Start is called before the first frame update
    void Start() {
        target = targetB;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        if (Vector2.Distance(transform.position, targetA.position) <= 0.1f) {
            target = targetB;
            spriteRenderer.flipX = false;
        }

        if (Vector2.Distance(transform.position, targetB.position) <= 0.1f) {
            target = targetA;
            spriteRenderer.flipX = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            collision.gameObject.SendMessage("OnDamage", attack_value);
        }

        if (target == targetA) {
            target = targetB;
            spriteRenderer.flipX = false;
        }
        else {
            target = targetA;
            spriteRenderer.flipX = true;
        }
    }
}