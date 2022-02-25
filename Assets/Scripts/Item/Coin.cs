using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Entity.Player;
using Item;
using UnityEngine;
using UnityEngine.UIElements;


namespace Item
{
    public class Coin : MonoBehaviour
    {
        public int frequency;
        private Vector3 Position;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerEntity>())
            {
                other.gameObject.GetComponent<PlayerEntity>().OnItemCollect(Items.Coin, 1);
                Destroy(gameObject);
            }
        }
    
        private void Start()
        {
            Position = transform.position;
        }
    
        private void Update()
        {
            transform.position = Position + transform.up * Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * 0.1f;
        }
    }
}

