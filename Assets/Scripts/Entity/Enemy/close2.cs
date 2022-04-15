using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entity.Enemy
{
    public class close2 : Entity
    {
        // Start is called before the first frame update
        [SerializeField] private float moveSpeed;
        private Transform target;
        public float attackDamage = 2;
        private bool found;
        public Transform firePoint;


        Rigidbody2D _rigidbody2D;
        // private SpriteRenderer _spriteRenderer;

        // [SerializeField] private bool isLeft;
        private void Awake()
        {
            
        }
        protected new void Start()
        {
            base.Start();
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            found = false;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected new void Update()
        {
            base.Update();
            if (found == true)
            {

                if (transform.position.x > target.position.x)
                {
                    SpriteRenderer.flipX = true;
                    _rigidbody2D.AddForce(Vector2.left * (8f * Time.deltaTime), ForceMode2D.Impulse);
                }
                else
                {
                    SpriteRenderer.flipX = false;
                    _rigidbody2D.AddForce(Vector2.right * (8f * Time.deltaTime), ForceMode2D.Impulse);
                }
            }


        }



        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("OnDamage", attackDamage);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                found = true;
            }
        }
    }
}
