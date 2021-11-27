using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D MyRigidbody;
    public Health MyHealth;
    public Animator animator;
    public List<EnemyMove> allMoves = new();

    [HideInInspector] public int type;
    [HideInInspector] public float AlertRange;
    [HideInInspector] public float AttackRange;
    [HideInInspector] public float distanceToPlayer;

    public Vector3 PlayerPosition()
    {
        return Player.Instance == null ? transform.position : Player.Instance.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Set component variables
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();

        // Set random enemy type
        //type = Random.Range(0, 3);
        type = 0;
        if (type == 0)
        {
            AttackRange = 0.8f;
        }
        else if (type == 1)
        {
            AttackRange = 3f;
        }

        AlertRange = AttackRange + 5f;

        distanceToPlayer = 0f;
        animator.SetBool("dead", false);
    }

    void Update()
    {
        if (MyHealth.currentHealth <= 0)
        {
            animator.SetBool("dead", true);
        }

        distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());
    }
}