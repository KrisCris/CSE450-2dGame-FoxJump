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

        // private SpriteRenderer _spriteRenderer;

        // [SerializeField] private bool isLeft;
        private void Awake()
        {
            
        }
        protected new void Start()
        {
            base.Start();
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected new void Update()
        {
            base.Update();
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, -1.4f, target.position.z), Time.deltaTime);
            
            if(transform.position.x > target.position.x)
            {
                SpriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer.flipX = false;
            }
        }

        

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("OnDamage", attackDamage);
            }
        }
        
    }
}
