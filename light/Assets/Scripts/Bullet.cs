﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    public float speed;
    public ParticleSystem VFX_Destroyed;
    public int baseDamage = 1;
    public int fireDamage = 2;
    int damage;

    public GameObject fireVFX;
    public bool onFire = false;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction*speed;
    }

    
    void Update()
    {
        damage = !onFire ? baseDamage : baseDamage + fireDamage;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hit = collision.GetComponent<IHitable>();
        if (hit != null) hit.OnHit(damage);
        Instantiate(VFX_Destroyed, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable hit = collision.gameObject.GetComponent<IHitable>();
        if (hit != null) hit.OnHit(damage);
        Instantiate(VFX_Destroyed, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetOnFire(bool fire) {
        onFire = fire;
        fireVFX.SetActive(onFire);
    }
}