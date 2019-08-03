using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Options")]
    public float speed;
    public float lifetime;
    public int rank;
    [Header("Visual")]
    public Sprite bulletSprite;
    public GameObject destroyEffect;
    public SpriteRenderer sr;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        InitFunction(rank, bulletSprite);
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Вызываем функцию урона врагу
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Boss"))
        {
            //Вызываем функцию урона боссу
        }
        else if(other.CompareTag("Shield"))
        {
            //Вызываем функцию отбрасывания
        }
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    public void InitFunction(int digit, Sprite sprite)
    {
        rank = digit;
        sr.sprite = sprite;
    }
}
