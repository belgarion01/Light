using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Ennemy : SerializedMonoBehaviour, IHitable
{
    [BoxGroup("Ennemy Stats")]
    public float maxHealth;
    private float health;
    public UnityEvent HitEvent;
    Animator anim;
    public float dieSpeed = 2.5f;
    bool die = false;

    public float hitTime = 0.05f;


    public void OnHit(int damage, bool onFire)
    {
        if (damage <= 0) return;
        HitEvent?.Invoke();
        StartCoroutine(TakeDamage(damage));
    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    IEnumerator TakeDamage(int damage) {
        health -= damage;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites) {
            sprite.material.SetFloat("_Hit", 1);
        }
        Debug.Log(gameObject.name + " has taken " + damage + " damage !");
        if (health <= 0) Die();
        yield return new WaitForSeconds(hitTime);
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Hit", 0);
        }
    }

    void Die() {
        //Destroy(gameObject);
        if(!die)StartCoroutine(DieAction());
    }

    IEnumerator DieAction() {
        die = true;
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;
        foreach (Transform child in transform)
        {
            Collider2D col = child.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
        float value = 0f;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Die", 1);
        }
        while (value < 1)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.material.SetFloat("_DiePercent", value);
                value += Time.deltaTime*dieSpeed;
                yield return new WaitForFixedUpdate();
            }
        }
        Destroy(gameObject);
        yield return null;
    }
}
