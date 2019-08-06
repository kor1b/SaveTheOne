using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header ("Movement")]
    public float speed;

    [Header("Visual")]
    public GameObject destroyEffect;
    public float deathDelay;
    public SpriteRenderer gunRenderer;

    SpriteRenderer sr;
    Vector2 movement;
    Animator animator;
    ParticleSystem ps;
    private Rigidbody2D rb;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = destroyEffect.GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * speed;

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            animator.ResetTrigger("RunRight");
            animator.ResetTrigger("Stay");
            animator.SetTrigger("RunLeft");
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.ResetTrigger("Stay");
            animator.ResetTrigger("RunLeft");
            animator.SetTrigger("RunRight");
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.ResetTrigger("RunRight");
            animator.ResetTrigger("RunLeft");
            animator.SetTrigger("Stay");
        }
        transform.localScale = characterScale;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.deltaTime * movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("Spike"))
        {   
            TakeDamage();
        }
    }

    void TakeDamage()
    { 
        ps.Play();
        sr.enabled = false;
        gunRenderer.enabled = false;
        Invoke("Death", 1f);
        GameManager.Instance.GameOver(deathDelay);
		enabled = false;
    }
    void Death()
    {
        gameObject.SetActive(false);
    }


}
