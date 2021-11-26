using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D MyRigidbody;
    [HideInInspector] public Transform MyTransform;
    public Health MyHealth;


    [HideInInspector] public GameObject player;
    [HideInInspector] public Transform PlayerTransform;

    [HideInInspector] public List<EnemyMove> allMoves = new();

    [HideInInspector] public int type;
    [HideInInspector] public float range;


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
        type = Random.Range(0, 2);
        if (type == 0) { range = 0.5f; }
        else if (type == 1) { range = 3f; }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
