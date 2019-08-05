﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    SpawnManager spawnManager;
	public int level = 1;

	#region Singleton
	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
    #endregion

    private void Start()
    {
        spawnManager = SpawnManager.Instance;
    }

    public void GameOver(float sceneReloadDelay)
    {
        StartCoroutine(ReloadScene(sceneReloadDelay));
    }

    IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
