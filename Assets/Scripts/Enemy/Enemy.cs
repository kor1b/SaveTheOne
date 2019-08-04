using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "EnemyObject")]
public class Enemy : ScriptableObject
{
	public Sprite sprite;
	public int rank;
	public float speed;
}
