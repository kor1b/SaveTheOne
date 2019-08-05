using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySoldier : EnemyCharacter
{
	public Enemy enemyScriptable;
	EnemyManager enemyManager;

	public SpriteRenderer enemyGFX;
	[Header ("Visual")]
	public GameObject healEffect;
	public GameObject deathEffect;
	ParticleSystem psHeal;
	private AIPath aiPath;
	//private int rank;
	private Animator animator;
	private void Awake()
	{
		psHeal = healEffect.GetComponent<ParticleSystem> ();
		psDeath = deathEffect.GetComponent<ParticleSystem> ();
		animator = enemyGFX.GetComponent<Animator> ();
		enemyManager = EnemyManager.Instance;

		aiPath = GetComponent<AIPath> ();
	}
	protected override void SetParameters()
	{
		rank = Random.Range (2, 9);
		enemyGFX.enabled = true;
		SwitchNumber ();
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
			Debug.Log ("Fuck!");
		}
	}

	public override void TakeDamage(int damage)
	{
		if (damage >= rank || damage == 1)
			Death ();
		else
			Heal ();
	}

	void Heal()
	{
		if (rank < 9)
		{
			//increase rank
			rank++;
			SwitchNumber ();
			psHeal.Stop ();
			psHeal.Play ();
		}
	}

	new void Death()
	{
		Debug.Log ("EnemyParticle");
		psDeath.Stop ();
		psDeath.Play ();
		enemyGFX.enabled = false;
		enemyManager.enemiesAlive.Remove (gameObject);

		if (enemyManager.enemiesAlive.Count == 3)
			enemyManager.spawnBigDigits = true;
		else if (enemyManager.enemiesAlive.Count == 0)
			enemyManager.SpawnBoss ();

		StartCoroutine (Deactivate ());
	}
}