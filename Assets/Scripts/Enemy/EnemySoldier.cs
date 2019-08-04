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

	int num = 2;

	protected override void SetParameters()
	{
		aiPath = GetComponent<AIPath> ();

		num = Random.Range (2, 9);

		SwitchNumber ();
	}

	void SwitchNumber()
	{
		enemyScriptable = EnemyManager.Instance.enemySoldiers[num - 2];

		rank = enemyScriptable.rank;
		enemyGFX.sprite = enemyScriptable.sprite;
		aiPath.maxSpeed = enemyScriptable.speed;
	}

	public override void TakeDamage(int damage)
	{
<<<<<<< Updated upstream
		if (damage >= enemyScriptable.rank)
=======
		if (damage >= rank || damage == 1)
>>>>>>> Stashed changes
			Death ();
		else
			Heal ();
	}

	void Heal()
	{
		//increase rank
		num++;
		SwitchNumber ();
		Debug.Log ("Rank " + rank);
	}
}
