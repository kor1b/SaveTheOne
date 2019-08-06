using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player"))
		{
			SpawnManager.Instance.enteredNewRoom = true;
		}
	}
}
