﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float shotSpeed;
    public float attackDamage = 2;
    [SerializeField] private float maxLife = 2.0f;
    private float lifeBtwTimer;
    public GameObject destoryEffect;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        //Always follow the player anytime
        transform.position = Vector2.MoveTowards(transform.position, target.position, shotSpeed * Time.deltaTime);

        lifeBtwTimer += Time.deltaTime;

        if (lifeBtwTimer >= maxLife)
        {
            //Instantiate(destoryEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            //Instantiate(destoryEffect, transform.position, Quaternion.identity);

            collision.gameObject.SendMessage("OnDamage", attackDamage);

            Destroy(gameObject);
        }
    }
    
   

}
