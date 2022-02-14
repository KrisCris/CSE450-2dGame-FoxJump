using System;
using UnityEngine;

namespace Code.Entity
{
    public class Entity : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody2D;
        protected Collider2D Collider2D;
        protected SpriteRenderer SpriteRenderer;

        public float maxHealth = 10;

        protected void GetObjects()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected void Start()
        {
            GetObjects();
        }

        private void Update()
        {
            // TODO Entity  Movement? 
            // TODO health management?
        }
    }
}