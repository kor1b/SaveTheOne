using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    SpawnManager spawnManager;
    public GameObject coridor;
	public int level = 1;
	public float score = 0;

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

    public void LoadCorridor()
    {
        spawnManager.AddNewCoridor(coridor);
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
