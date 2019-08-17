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

    public float hitTime;


    public void OnHit(int damage)
    {
        HitEvent.Invoke();
        StartCoroutine(TakeDamage(damage));
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

    IEnumerator TakeDamage(int damage) {
        health -= damage;
        GetComponent<SpriteRenderer>().material.SetFloat("_Hit", 1);
        Debug.Log(GetComponent<SpriteRenderer>().material.GetFloat("_Hit"));
        Debug.Log(gameObject.name + " has taken " + damage + " damage !");
        if (health <= 0) Die();
        yield return new WaitForSeconds(hitTime);
        GetComponent<SpriteRenderer>().material.SetFloat("_Hit", 0);
    }

    void Die() {
        Destroy(gameObject);
    }
}
