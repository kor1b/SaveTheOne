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
}
