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
    Vector2 movement;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput * speed;
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
        gameObject.SetActive(false);
        //Вызвать партикл смерти игрока
        StartCoroutine(ReloadScene(deathDelay));
    }

    IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
