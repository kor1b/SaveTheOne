using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyCharacter
{
    public int numberOfShieldsLeft = 3;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetParameters();
    }

    protected override void SetParameters()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        gameObject.SetActive(false);
    }
    public void PushAway(Vector2 direction, float forceCoeficient)
    {
        rb.AddForce(direction * forceCoeficient, ForceMode2D.Impulse);
        rb.AddTorque(direction.x, ForceMode2D.Impulse);
    }
}
