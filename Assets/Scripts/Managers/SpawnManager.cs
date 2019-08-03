using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager Instance;

	#region Singleton
	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
	#endregion
}
