﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch : MonoBehaviour
{
    private float moveRate = 2.0f;
    private float moveTimer;

    private float shotRate = 2.1f;
    private float shotTimer;
    public GameObject projectile;

    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private string enemyName;
    [SerializeField] protected private float moveSpeed;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;

    protected private Transform target;//The Target is our player
    [SerializeField] protected private float distance;
    private SpriteRenderer sp;

    public Image hpImage;//Red Health Bar
    public Image hpEffectImage;//White Health Bar Hurting Effect

    private void Start()
    {
        healthPoint = maxHealthPoint;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();

        Introduction();
    }

    private void Update()
    {
        TurnDirection();

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }

        Attack();
        
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void TurnDirection()
    {
        if (transform.position.x > target.position.x)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }
    protected void Introduction()
    {
        Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    protected void Move()
    {
        //base.Move();//MARKER Give up the base Move Function!!
        RandomMove();
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;
       
        if (moveTimer > moveRate)
        {
            transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            moveTimer = 0;
        }
    }

    protected void Attack()
    {
        Debug.Log(enemyName + " is Attacking");

        shotTimer += Time.deltaTime;

        if(shotTimer > shotRate)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotTimer = 0;
        }
    }

}
