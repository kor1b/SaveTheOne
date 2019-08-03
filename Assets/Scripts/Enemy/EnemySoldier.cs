using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : EnemyCharacter
{
	private int rank = 2;               //rank of number

	protected override void SetParameters()
	{
		rank = Random.Range (2, 9);
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
