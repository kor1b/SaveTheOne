using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySoldier : EnemyCharacter
{
    private Enemy enemyScriptable;

    public SpriteRenderer enemyGFX;
    private AIPath aiPath;
    private int rank;
    private Animator animator;
    //private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        

        aiPath = GetComponent<AIPath>();
    }
    protected override void SetParameters()
    {
        rank = Random.Range(2, 9);

        SwitchNumber();
    }

    void SwitchNumber()
    {
        enemyScriptable = EnemyManager.Instance.enemySoldiers[rank - 2];
        enemyGFX.sprite = enemyScriptable.sprite;
        aiPath.maxSpeed = enemyScriptable.speed;
        if (enemyScriptable.animator != null)
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
        EnemyManager.Instance.enemiesAlive.Remove(gameObject);
    }
}