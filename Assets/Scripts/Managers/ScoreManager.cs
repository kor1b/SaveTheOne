using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	private float score = 0;

	[SerializeField] private TextMeshProUGUI scoreText;

	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}

	public void AddScore(int value)
	{
		StartCoroutine (AddScoreCoroutine (value));
	}

	IEnumerator AddScoreCoroutine(int value)
	{
		while (value > 0)
		{
			Debug.Log (value);
			score++;
			scoreText.text = score.ToString ();
			value--;
			yield return new WaitForSeconds (.05f);
		}
	}
}
