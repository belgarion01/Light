using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IFire
{
    public Vector2 direction = Vector2.zero;
    public float speed;
    public ParticleSystem VFX_Destroyed;
    public ParticleSystem VFX_OnFiredUp;
    public int baseDamage = 1;
    public int fireDamage = 2;
    int damage;

    public GameObject fireVFX;
    public bool onFire = false;

    Rigidbody2D rb2d;

    public float deathTime = 5f;
    public float test;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction*speed;
    }

    
    void Update()
    {
        if (deathTime > 0)
        {
            deathTime -= Time.deltaTime;
        }
        else {
            //Instantiate(VFX_Destroyed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        damage = !onFire ? baseDamage : fireDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable hit = collision.gameObject.GetComponent<IHitable>();
        if (hit != null) hit.OnHit(damage, onFire);
        Instantiate(VFX_Destroyed, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IHitable hit = collision.gameObject.GetComponent<IHitable>();
        //if (hit != null) hit.OnHit(damage, onFire);
        //Instantiate(VFX_Destroyed, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }

    public void SetFire(bool value)
    {
        if (value)
        {
            ParticleSystem vfx = Instantiate(VFX_OnFiredUp, transform.position+(-transform.up*test), Quaternion.identity);
            Destroy(vfx, 2f);
        }
        onFire = value;
        fireVFX.SetActive(onFire);
    }
}