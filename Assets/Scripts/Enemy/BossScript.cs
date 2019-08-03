using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemySoldier
{
    List<GameObject> shields = new List<GameObject>();
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetParameters();
    }

    public void PushAway(Vector2 direction, float forceStrength)
    {
        rb.AddForce(direction*forceStrength, ForceMode2D.Impulse);
        rb.AddTorque(direction.x, ForceMode2D.Impulse);
    }
}
