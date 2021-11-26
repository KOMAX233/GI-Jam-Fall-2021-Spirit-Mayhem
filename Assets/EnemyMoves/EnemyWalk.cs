using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyMove
{
    public float speed = 1f;
    public float distance;

    private int type;
    public float range;
    private GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        // Sets the enemy type
        type = enemy.type;
        // Initialize range
        range = enemy.range;
        distance = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!IsActive) return;
        distance = Vector2.Distance(enemy.MyTransform.position, enemy.PlayerTransform.position);
        if (distance > range)
        {
            enemy.MyTransform.position = Vector2.MoveTowards(enemy.MyTransform.position, enemy.PlayerTransform.position, speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        if (range == 0) { range = enemy.range; }
        if (enemy.allMoves.Exists(m => m != this && m.IsActive))
        {
            EndMove();
        }
        else
        {
            TryStartMove();
        }
    }
}
