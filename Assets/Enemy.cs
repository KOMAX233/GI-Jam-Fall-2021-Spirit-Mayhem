using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D MyRigidbody;
    [HideInInspector] public Transform MyTransform;
    public Health MyHealth;
    public Animator animator;

    [HideInInspector] public GameObject player;
    [HideInInspector] public Transform PlayerTransform;

    public List<EnemyMove> allMoves = new();

    [HideInInspector] public int type;
    [HideInInspector] public float AlertRange;
    [HideInInspector] public float AttackRange;
    [HideInInspector] public float distanceToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        // Set component variables
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyTransform = GetComponent<Transform>();
        MyHealth = GetComponent<Health>();

        // Set Player object
        player = GameObject.FindWithTag("Player");
        PlayerTransform = player.GetComponent<Transform>();

        // Set random enemy type
        //type = Random.Range(0, 3);
        type = 0;
        if (type == 0) { AttackRange = 0.8f; }
        else if (type == 1) { AttackRange = 3f; }
        AlertRange = AttackRange + 5f;

        distanceToPlayer = 0f;
        animator.SetBool("dead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MyHealth.currentHealth <= 0) {
            animator.SetBool("dead", true);
        }
        distanceToPlayer = Vector2.Distance(MyTransform.position, PlayerTransform.position);
    }
}
