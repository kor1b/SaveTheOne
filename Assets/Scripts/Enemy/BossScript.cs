using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemySoldier
{
    public int numberOfShieldsLeft = 3;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetParameters();
    }

    public void PushAway(Vector2 direction, float forceCoeficient)
    {
        rb.AddForce(direction*forceCoeficient, ForceMode2D.Impulse);
        rb.AddTorque(direction.x, ForceMode2D.Impulse);
    }
}
