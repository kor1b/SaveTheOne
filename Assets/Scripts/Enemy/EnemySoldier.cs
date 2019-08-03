using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : EnemyCharacter
{
	public SpriteRenderer enemyGFX;

	private int rank = 2;               //rank of number

	protected override void SetParameters()
	{
		rank = Random.Range (2, 9);
		Debug.Log (rank);
		enemyGFX.sprite = EnemyManager.Instance.enemySoldierSprites[rank - 2];
		rank += 2;
	}

	public override void TakeDamage(int damage)
	{
		if (damage >= rank)
			Death ();
		else
			Heal ();
	}

	void Heal()
	{
		//increase rank
		rank++;
		Debug.Log ("Rank " + rank);
	}
}
