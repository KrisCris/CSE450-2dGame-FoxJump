using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Code.Entity.Player;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin : MonoBehaviour
{
    public int frequency;
    private Vector3 Position;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
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
