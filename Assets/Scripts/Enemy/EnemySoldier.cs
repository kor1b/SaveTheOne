using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySoldier : EnemyCharacter
{
    public Enemy enemyScriptable;

    public SpriteRenderer enemyGFX;
    private AIPath aiPath;
    private int rank;

    protected override void SetParameters()
    {
        aiPath = GetComponent<AIPath>();

        rank = Random.Range(2, 9);

        SwitchNumber();
    }

    void SwitchNumber()
    {
        enemyScriptable = EnemyManager.Instance.enemySoldiers[rank - 2];

        enemyGFX.sprite = enemyScriptable.sprite;
        aiPath.maxSpeed = enemyScriptable.speed;
    }

    public override void TakeDamage(int damage)
    {
            if (damage >= rank || damage == 1)
                Death();
            else
                Heal();
    }

    void Heal()
    {
        if (rank < 9)
        {
            //increase rank
            rank++;
            SwitchNumber();
            Debug.Log("Rank " + rank);
        }
    }

    new void Death()
    {
        base.Death();
        EnemyManager.Instance.enemiesAlive.Remove(gameObject);
    }
}