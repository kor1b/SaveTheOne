using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySoldier : EnemyCharacter
{
    public Enemy enemyScriptable;
    EnemyManager enemyManager;

    public SpriteRenderer enemyGFX;
    private AIPath aiPath;
    private int rank;
    private Animator animator;
    private void Awake()
    {
        animator = enemyGFX.GetComponent<Animator>();
        enemyManager = EnemyManager.Instance;

        aiPath = GetComponent<AIPath>();
    }
    protected override void SetParameters()
    {
        rank = Random.Range(2, 9);

        SwitchNumber();
    }

    void SwitchNumber()
    {
        enemyScriptable = enemyManager.enemySoldiers[rank - 2];

        enemyGFX.sprite = enemyScriptable.sprite;
        aiPath.maxSpeed = enemyScriptable.speed;
        if (enemyScriptable.animator != null || animator != null)
        {
            animator.runtimeAnimatorController = enemyScriptable.animator;
        }
        else
        {
            Debug.Log("Fuck!");
        }
        
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
        enemyManager.enemiesAlive.Remove(gameObject);

        //if (enemyManager.enemiesAlive.Count == 3)
            //enemyManager.spawnBigDigits = true;
    }
}