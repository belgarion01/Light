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

    public float hitTime = 0.05f;


    public void OnHit(int damage, bool onFire)
    {
        HitEvent.Invoke();
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
        Destroy(gameObject);
    }

    IEnumerator DieAction() {
        float value = 0f;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites) {
            //sprite.material.SetFloat(
            yield return new WaitForFixedUpdate();
        }
    }
}
