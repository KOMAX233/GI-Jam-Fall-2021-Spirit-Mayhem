using UnityEngine;

public class EnemyWalk : EnemyMove
{
    public float speed = 1f;

    private int type;
    public float AttackRange;
    public float AlertRange;
    private GameObject player;


    private void Start()
    {
        // Sets the enemy type
        type = enemy.type;

        // Initialize ranges
        AttackRange = enemy.AttackRange;
        AlertRange = enemy.AlertRange;
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        // If enemy is between AlertRange and AttackRange
        if (enemy.distanceToPlayer > AttackRange && enemy.distanceToPlayer <= AlertRange)
        {
            // Move enemy closer to player
            // enemy.MyTransform.position = Vector2.MoveTowards(enemy.MyTransform.position, enemy.PlayerTransform.position, speed * Time.deltaTime);
            enemy.MyRigidbody.velocity = (enemy.PlayerTransform.position - enemy.transform.position).normalized * speed;
        }
    }
    private void Update()
    {
        // Update AttackRange and AlertRange in case of changes
        if (AttackRange == 0) { AttackRange = enemy.AttackRange; }
        if (AlertRange == 0) { AlertRange = enemy.AlertRange; }

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
