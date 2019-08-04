using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTrigger : MonoBehaviour
{
    Transform player;
    AIPath aiPath;

    public float triggerDistance;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        aiPath = GetComponent<AIPath>();
    }
    private void Update()
    {
        if (aiPath.enabled)
            return;

        if (Vector2.Distance(transform.position, player.position) < triggerDistance)
            aiPath.enabled = true;
    }
}
