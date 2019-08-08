using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    _SpawnDigit digitSpawner;

	Transform gun;

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
		gun = GameObject.FindWithTag ("Gun").transform;
    }
    private void OnEnable()
    {
        InitFunction(digitSpawner.GetDigitActive()+1, digitSpawner.GetSpriteActiveDigit());
        lifeTimer = lifetime;

		rb.AddForce (gun.right * speed * 50);
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
	//private void FixedUpdate()
	//{
	//    rb.velocity = transform.right * speed;
	//}

	private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject != null && other.gameObject.activeInHierarchy)
            {
                other.gameObject.GetComponent<EnemySoldier>().TakeDamage(rank);
                DestroyBullet();
            }
           
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            if (rank == 1)
            {
                other.gameObject.GetComponent<BossScript>().TakeDamage(rank);
                DestroyBullet();
            }
        }
        else if(other.gameObject.CompareTag("Shield"))
        {
            if (rank == 1)
            {
                Vector2 dir = other.transform.position - transform.position;
                other.gameObject.GetComponentInParent<BossScript>().PushAway(dir, 10f);
            }
            DestroyBullet();
        }
		if (rank != 1)
		{
			DestroyBullet ();
			//Вызвать партикл уничтожения пули

		}
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
