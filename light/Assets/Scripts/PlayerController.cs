using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Animator anim;
    SpriteRenderer renderer;
    public UnityEvent OnHit;

    float hAxis;

    public float movementSpeed;

    public float jumpForce;
    public float jumpTime;
    float currentJumpTime;
    bool isJumping = false;//

    public Transform groundCheckPosition; 
    bool grounded;
    bool wasGrounded;
    public float groundCheckRadius;
    public LayerMask m_Ground;

    [HideInInspector]
    public bool isRight = true;

    public UnityEvent OnFoot;
    public UnityEvent OnJump;

    AudioSource source;

    public SoundAsset walkSound;
    public SoundAsset jumpSound;

    bool invulnerable = false;

    public bool gizmos;
    public float invulnerableTime;
    int health = 3;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        HandleJump();
        CheckForCollisions();
        renderer.material.SetFloat("_Hit", invulnerable ? 1f : 0f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void CheckForCollisions() {
        grounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, m_Ground);
        anim.SetBool("Grounded", grounded);
        //wasGrounded = grounded;
    }

    void Move() {
        Vector2 targetVelocity = new Vector2();
        targetVelocity.x = hAxis * movementSpeed * Time.fixedDeltaTime;
        targetVelocity.y = rb2d.velocity.y;
        rb2d.velocity = targetVelocity;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((isRight && mousePosition.x < transform.position.x) || (!isRight && mousePosition.x > transform.position.x)) Flip();
        if (targetVelocity.x == 0) anim.SetBool("isMoving", false);
        else anim.SetBool("isMoving", true);
    }

    void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            isJumping = true;
            currentJumpTime = jumpTime;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        if (Input.GetKey(KeyCode.Space)&&isJumping)
        {
            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    public void PlayWalkSound() {
        walkSound.Play(source);
    }

    public void PlayJumpSound() {
        jumpSound.Play(source);
    }

    void Flip() {
        Vector3 targetScale = transform.localScale;
        targetScale.x *= -1;
        transform.localScale = targetScale;
        isRight = !isRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos) {
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
    }

    public void TakeDamageAction() {
        if (invulnerable) return;
        StartCoroutine(TakeDamage(1));
    }

    IEnumerator TakeDamage(int damage)
    {
        OnHit?.Invoke();
        health -= damage;
        UpdateUI();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        invulnerable = true;
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Hit", 1);
        }
        Debug.Log(gameObject.name + " has taken " + damage + " damage !");
        if (health <= 0) Die();
        yield return new WaitForSeconds(invulnerableTime);

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.material.SetFloat("_Hit", 0);
        }
        invulnerable = false;
    }

    public void Die() {
        FindObjectOfType<GameManager>().ShowDeathMenu(true);
    }

    void UpdateUI() {
        switch (health) {
            case 2: life3.SetActive(false);
                break;
            case 1: life2.SetActive(false);
                break;
            case 0: life1.SetActive(false);
                break;
        }
    }
}
