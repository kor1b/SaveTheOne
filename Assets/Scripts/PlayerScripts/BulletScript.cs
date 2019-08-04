using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    _SpawnDigit digitSpawner;

    [Header("Options")]
    public float speed;
    public float lifetime;
    public float knockbackForse;
    public int rank;
    [Header("Visual")]
    public Sprite bulletSprite;
    public GameObject destroyEffect;
    public SpriteRenderer sr;
    Rigidbody2D rb;
    private float lifeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        digitSpawner = _SpawnDigit.Instance;
    }
    private void OnEnable()
    {
        InitFunction(digitSpawner.GetDigitActive()+1, digitSpawner.GetSpriteActiveDigit());
        lifeTimer = lifetime;
    }

    private void Update()
    {
        if (lifeTimer <= 0)
        {
            DestroyBullet();
        }
        else
        {
            lifeTimer -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject != null && other.gameObject.activeInHierarchy)
            {
                other.GetComponent<EnemySoldier>().TakeDamage(rank);
                DestroyBullet();
            }
           
        }
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossScript>().TakeDamage(rank);
            DestroyBullet();
        }
        else if(other.CompareTag("Shield"))
        {
            if (rank == 1)
            {
                Vector2 dir = other.transform.position - transform.position;
                other.GetComponentInParent<BossScript>().PushAway(dir, 10f);
            }
            DestroyBullet();
        }
        //if (rank != 1)
        //{
            DestroyBullet();
            //Вызвать партикл уничтожения пули
        //}
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
        //Вызвать партикл уничтожения пули
    }

    public void InitFunction(int digit, Sprite sprite)
    {
        rank = digit;
        bulletSprite = sprite;
        sr.sprite = bulletSprite;
    }
}
