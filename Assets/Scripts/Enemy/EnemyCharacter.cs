using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
	public int rank = 0;

	protected ParticleSystem psDeath;

	private void OnEnable()
	{
		SetParameters ();
	}

	protected abstract void SetParameters();

	public abstract void TakeDamage(int damage);

	protected void Death()
	{
		GetComponent<Collider2D> ().enabled = false;
		gameObject.SetActive (false);
		Debug.Log ("Killed " + gameObject.name);
	}

	protected IEnumerator Deactivate()
	{
		ScoreManager.Instance.AddScore (rank);
		yield return new WaitForSeconds (psDeath.main.startLifetime.constantMax);
		Death ();
	}
}
