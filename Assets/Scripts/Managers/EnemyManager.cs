using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance;

	#region Singleton
	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
	#endregion

	[Header("Enemies")]
	public GameObject enemySoldierPrefab;
	public int maxEnemiesCount = 15;
	public int extraEnemies = 4;
	int enemiesCount;

	[Header("Distances")]
	//min/max distance from spawn position
	public float minOffsetX = 0;
	public float maxOffsetX = 4;
	public float minOffsetY = 0;
	public float maxOffsetY = 4;


	public Transform[] spawnPositions;

	private void Start()
	{
		SpawnEnemies (0);
	}

	void SpawnEnemies(int level)
	{
		enemiesCount = level + extraEnemies;

		for (int i = 0; i < spawnPositions.Length; i++)
		{
			int enemiesPerPoint = enemiesCount / spawnPositions.Length;

			for (int j = 0; j < enemiesPerPoint; j++)
			{
				float offset_X = Random.Range (minOffsetX, maxOffsetX);
				float offset_Y = Random.Range (minOffsetY, maxOffsetY);

				Vector2 spawnPos = (Vector2)spawnPositions[i].position + new Vector2 (offset_X, offset_Y);
				//TODO: Instantiate enemy
				//Instantiate(enemySoldierPrefab, spawnPos, Quaternion.identity)
			}
		}

		//TODO:Instantiate boss
	}
}
