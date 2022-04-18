using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch : MonoBehaviour {
    private float moveRate = 2.0f;
    private float _moveTimer;

    public float shootDelay = 4f;
    private float _shootTimer;
    public GameObject projectile;

    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private string enemyName;
    [SerializeField] protected private float moveSpeed;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;

    protected private Transform target; //The Target is our player
    [SerializeField] protected private float distance;
    private SpriteRenderer sp;

    public Image hpImage; //Red Health Bar
    public Image hpEffectImage; //White Health Bar Hurting Effect

    private bool found;

    private void Start() {
        healthPoint = maxHealthPoint;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();

        found = false;

        // Introduction();
    }

    private void Update() {
        TurnDirection();

        if (healthPoint <= 0) {
            Destroy(gameObject);
        }

        if (found == true) {
            Attack();
        }
    }

    private void FixedUpdate() {
        if (found == true) {
            Move();
        }
    }

    private void TurnDirection() {
        if (target) {
            if (transform.position.x > target.position.x) {
                sp.flipX = true;
            }
            else {
                sp.flipX = false;
            }
        }
    }

    protected void Introduction() {
        Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    protected void Move() {
        //base.Move();//MARKER Give up the base Move Function!!
        RandomMove();
    }

    private void RandomMove() {
        _moveTimer += Time.deltaTime;

        if (_moveTimer > moveRate) {
            transform.position = new Vector3(target.position.x + Random.Range(minX, maxX),
                target.position.y + Random.Range(minY, maxY), 0);
            _moveTimer = 0;
        }
    }

    protected void Attack() {
        // Debug.Log(enemyName + " is Attacking");

        _shootTimer += Time.deltaTime;

        if (_shootTimer > shootDelay) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            _shootTimer = 0;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision) {
        // Vector3 v3 = new Vector3(target.position.x + Random.Range(minX, maxX),
        //     target.position.y + Random.Range(minY, maxY), 0);
        // Debug.Log("meh");
        // transform.position = v3;
    }

    // private void OnTriggerEnter2D(Collider2D collision) {
    //     if (collision.CompareTag("Player")) {
    //         found = true;
    //     } else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
    //         Vector3 v3 = new Vector3(target.position.x + Random.Range(minX, maxX),
    //             target.position.y + Random.Range(minY, maxY), 0);
    //         Debug.Log("meh");
    //         transform.position = v3;
    //     }
    // }
}