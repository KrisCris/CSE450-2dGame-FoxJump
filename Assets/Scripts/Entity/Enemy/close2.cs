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
            // _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected new void Update()
        {
            base.Update();
            if (found == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime);

                if (transform.position.x > target.position.x)
                {
                    SpriteRenderer.flipX = true;
                }
                else
                {
                    SpriteRenderer.flipX = false;
                }
            }

            RaycastHit2D hitInfo;

            hitInfo = Physics2D.Raycast(transform.position, -1*transform.up, 1f);

            if (hitInfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.green);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f*Time.deltaTime, 0);
                found = false;
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
