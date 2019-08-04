using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossScript : EnemyCharacter
{
    AIDestinationSetter aiDSsetter;
    public int numberOfShieldsLeft = 3;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindWithTag("Player").activeInHierarchy)
        aiDSsetter.target = GameObject.FindWithTag("Player").transform;
        //SetParameters();
    }

    protected override void SetParameters()
    {
        numberOfShieldsLeft = EnemyManager.Instance.shieldsCount;
    }

    public override void TakeDamage(int damage)
    {
        GameManager.Instance.LoadCorridor();
        Death();

    }
    public void PushAway(Vector2 direction, float forceCoeficient)
    {
        rb.AddForce(direction * forceCoeficient, ForceMode2D.Impulse);
        rb.AddTorque(direction.x, ForceMode2D.Impulse);
    }
}
