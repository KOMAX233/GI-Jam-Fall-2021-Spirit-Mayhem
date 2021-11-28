using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D MyRigidbody;
    [HideInInspector] public Health MyHealth;
    public Animator animator;
    public List<EnemyMove> allMoves = new();

    public GameObject deadEnemyPrefab;
    public float deadEnemyLifetime = 1f;

    [HideInInspector] public int type;
    public float MAlertRange;
    public float RAlertRange;
    public float MAttackRange;
    public float RAttackRange;

    public Vector3 PlayerPosition()
    {
        return Player.Instance == null ? transform.position : Player.Instance.transform.position;
    }

    public float DistanceToPlayer => Vector2.Distance(transform.position, PlayerPosition());
    
    private void Start()
    {
        // Set component variables
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();

        // Set random enemy type
        type = Random.Range(0, 3);
        if (type == 0 || type == 2)
        {
            MAttackRange = 0.8f;
        }

        if (type == 1 || type == 2)
        {
            RAttackRange = 3.9f;
        }

        MAlertRange = MAttackRange + 3f;
        RAlertRange = RAttackRange + 3f;

        animator.SetBool("dead", false);
    }

    public void SpawnDeadEnemy()
    {
        var deadEnemy = Instantiate(deadEnemyPrefab);
        deadEnemy.transform.position = transform.position;
        Destroy(deadEnemy, deadEnemyLifetime);
    }
}