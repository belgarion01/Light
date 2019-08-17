using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ennemy : MonoBehaviour, IHitable
{
    public float maxHealth;
    private float health;
    public UnityEvent HitEvent;
    Animator anim;


    public void OnHit(int damage)
    {
        HitEvent.Invoke();
        TakeDamage(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(int damage) {
        health -= damage;
        anim.SetTrigger("Hit");
        Debug.Log(gameObject.name + " has taken " + damage + " damage !");
        if (health <= 0) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
}
