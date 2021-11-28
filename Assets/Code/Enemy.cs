using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D MyRigidbody;
    public Health MyHealth;
    public Animator animator;
    public List<EnemyMove> allMoves = new();

    [HideInInspector] public int type;
    public float MAlertRange;
    public float RAlertRange;
    public float MAttackRange;
    public float RAttackRange;
    public float distanceToPlayer;

    

    public Vector3 PlayerPosition()
    {
        return Player.Instance == null ? transform.position : Player.Instance.transform.position;
    }


    void Start()
    {
        // Set component variables
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();

        // Set random enemy type
        //type = Random.Range(0, 2);
        type = 2;
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