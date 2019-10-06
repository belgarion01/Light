using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour, IHitable
{
    bool isLookingRight = false;
    public bool active = false;
    public UnityEvent OnBossBegin;
    public GameObject player;
    public int health = 50;
    public bool invulnerable = true;
    public GameObject Projectile;
    public float projSpeed;

    public SpriteRenderer[] ToAplha;
    public enum Phase { Phase1, Phase2, Phase3 }
    public Phase currentPhase = Phase.Phase1;

    public float timeToDo = 7f;
    private float currentTime = 0f;

    public Fire[] Torches;

    private Vector3 dirToPlayer;

    public GameObject Blocks;
    public float blockSpeed;
    public bool gizmos;
    public Transform boxOrigin;
    public Vector3 boxSize;

    public void BeginBoss() {
        OnBossBegin?.Invoke();
    }

    private void Update()
    {
        if (!active || Time.timeScale == 0) return;
        LookAtPlayer();
        dirToPlayer = (player.transform.position - transform.position).normalized;

        if (currentTime < timeToDo)
        {
            currentTime += Time.deltaTime;
        }
        else {
            DoSomething();
            currentTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            Transparent(true);
        }

        if (currentPhase == Phase.Phase1 && health < 40f) {
            ToPhase2();
        }

        if (currentPhase == Phase.Phase2 && health < 20f) {
            ToPhase3();
        }

        Transparent(!AllTorchesOn());
    }

    IEnumerator SprayShoot() {
        for (int i = 0; i < 8; i++)
        {
            ShootToPlayer();
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    IEnumerator FallingBlocks() {
        for (int i = 0; i < 8; i++)
        {
            SpawnBlocks();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnBlocks() {
        float randomValue = Random.Range(boxOrigin.position.x - (boxSize.x / 2), boxOrigin.position.x + (boxSize.x / 2));
        Vector3 position = boxOrigin.position;
        position.x = randomValue;
        GameObject block = Instantiate(Blocks, position, Quaternion.identity);
        block.GetComponent<Rigidbody2D>().velocity = Vector3.down * blockSpeed;
    }


    public void ShootToPlayer()
    {
        GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = dirToPlayer * projSpeed;
    }

    public void DoSomething() {
        StartCoroutine(FallingBlocks());
    }

    public void ShutDownTorches() {
        int index = Random.Range(0, Torches.Length - 1);
        for (int i = 0; i < Torches.Length; i++)
        {
            if (i != index) {
                Torches[i].Active = false;
            }
        }
    }

    private void LookAtPlayer() {
        if ((isLookingRight && dirToPlayer.x <= 0) || (!isLookingRight && dirToPlayer.x > 0)) {
            Flip();
        }
    }

    private void Flip() {
        Vector3 tScale = transform.localScale;
        tScale.x *= -1;
        transform.localScale = tScale;
        isLookingRight = !isLookingRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos) {
            Gizmos.DrawCube(boxOrigin.position, boxSize);
        }
    }

    void Transparent(bool value) {
        if (value)
        {
            foreach (SpriteRenderer sprite in ToAplha)
            {
                Color c = Color.white;
                c.a = 0.5f;
                sprite.color = c;
            }
        }
        else {
            foreach (SpriteRenderer sprite in ToAplha)
            {
                Color c = Color.white;
                c.a = 1f;
                sprite.color = c;
            }
        }
        invulnerable = value;
        GetComponent<Collider2D>().isTrigger = value;
    }

    bool AllTorchesOn() {
        bool value = true;
        foreach (Fire torche in Torches) {
            if (!torche.Active) {
                value = false;
            }
        }
        return value;
    }

    public void OnHit(int damage, bool onFire)
    {

        if (invulnerable) return;
        if (damage <= 0) return;
        StartCoroutine(TakeDamage(damage));

    }

    IEnumerator TakeDamage(int damage) {
        health -= damage;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Hit", 1);
        }

        //if (health <= 0) Die();
        yield return new WaitForSeconds(0.05f);
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Hit", 0);
        }
    }

    void ToPhase2() {
        ShutDownTorches();
        currentPhase = Phase.Phase2;
    }

    void ToPhase3() {
        ShutDownTorches();
        blockSpeed *= 1.5f;
        projSpeed *= 1.5f;
        currentPhase = Phase.Phase3;
    }
}
