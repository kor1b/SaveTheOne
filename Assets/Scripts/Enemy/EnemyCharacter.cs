using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
	private void OnEnable()
	{
		SetParameters ();
	}

	protected abstract void SetParameters();

	public abstract void TakeDamage(int damage);

	protected void Death()
	{
		//TODO: return to pool
		Debug.Log ("Killed " + gameObject.name);
	}

	
}
