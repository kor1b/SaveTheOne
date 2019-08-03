using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [Header ("Shooting")]
    public float startTimeBetweenShots = 2f;
    private float timeBeteweenShots;
    public GameObject bulletBrefab;
    public Transform bulletPool;
    public Transform shootPoint;
    ObjectPoolingManager pool;

    private void Start()
    {
        pool = ObjectPoolingManager.Instance;
        timeBeteweenShots = startTimeBetweenShots;
    }
    void Update()
    {
        if (timeBeteweenShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                timeBeteweenShots = startTimeBetweenShots;
            }
        }
        else
        {
            timeBeteweenShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject bullet = pool.GetObject(bulletBrefab, bulletPool, shootPoint.position, pool.bullets);
        bullet.transform.rotation = shootPoint.rotation;
    }
}
