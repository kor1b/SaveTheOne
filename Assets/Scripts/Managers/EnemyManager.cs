using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	ObjectPoolingManager pool;
	public static EnemyManager Instance;

	#region Singleton
	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
	#endregion

	public Transform enemyHolder;

	public bool spawnBigDigits = false;

	public List<GameObject> enemiesAlive;

	[Header ("Resourses")]
	public GameObject enemySoldierPrefab;
	public GameObject bossPrefab;
	public Enemy[] enemySoldiers;

	[Header ("Enemy Parameters")]
	public float maxSpeed = 5;
	public float maxAcceleration = 20;

	[Header ("Enemies on level")]
	public int maxEnemiesCount = 15;
	public int extraEnemies = 4;
	int enemiesCount;

	public int shieldsCount = 0;
	public int maxShieldsCount = 20;

	[Header ("Distances")]
	//min/max distance from spawn position
	public float minOffsetX = 0;
	public float maxOffsetX = 4;
	public float minOffsetY = 0;
	public float maxOffsetY = 4;


	public Transform[] spawnPositions;
	public Transform bossPosition;

	private void Start()
	{
		pool = ObjectPoolingManager.Instance;
		SpawnEnemies (0);
	}

	public void SpawnEnemies(int level)
	{
		spawnBigDigits = false;

		//random instantiate enemies
		int randomCountOfEnemies = Random.Range (0, 3);

		enemiesCount = level + extraEnemies + randomCountOfEnemies;
		//set max count for enemies
		if (enemiesCount > maxEnemiesCount)
			enemiesCount = maxEnemiesCount;

		float randomCountOfShields = Random.Range (0.7f, 1f);

		//set shields for boss
		shieldsCount = (int)( level * randomCountOfShields );
		if (shieldsCount > maxShieldsCount)
			shieldsCount = maxShieldsCount;

		for (int i = 0; i < spawnPositions.Length; i++)
		{
			int enemiesPerPoint = enemiesCount / spawnPositions.Length;

			for (int j = 0; j < enemiesPerPoint; j++)
			{
				float offset_X = Random.Range (minOffsetX, maxOffsetX);
				float offset_Y = Random.Range (minOffsetY, maxOffsetY);

				Vector2 spawnPos = (Vector2)spawnPositions[i].position + new Vector2 (offset_X, offset_Y);

				enemiesAlive.Add (pool.GetObject (enemySoldierPrefab, enemyHolder, spawnPos, pool.enemies));
			}
		}
	}

	public int GetMaxRank()
	{
		int maxRank = 2;

		foreach (var n in enemiesAlive)
		{
			int rank = n.GetComponent<EnemySoldier> ().enemyScriptable.rank;
			if (rank > maxRank)
				maxRank = rank;
		}

		return maxRank;
	}

	public void SpawnBoss()
	{
		pool.GetObject (bossPrefab, enemyHolder, bossPosition.position, pool.bosses);
	}
}
