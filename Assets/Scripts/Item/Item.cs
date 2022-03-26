﻿using System;
using Entity.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Item {
    public class Item : MonoBehaviour{
        public int frequency;
        protected Vector3 Position;
        protected bool IsFloat = true;
        protected Items ItemClass = Items.Default;

        private void Start() {
            Position = transform.position;
        }

        private void Update() {
            if (IsFloat) {
                transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerEntity>() && (!GetComponent<Rigidbody2D>() || GetComponent<Rigidbody2D>().velocity.magnitude < 1f)) {
                other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(ItemClass, 1);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Player") {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
            }
        }
    }
}