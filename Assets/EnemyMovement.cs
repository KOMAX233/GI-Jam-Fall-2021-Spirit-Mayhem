using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1;

    private Rigidbody2D myRigidbody;
    private Transform myTransform;

    private GameObject player;
    private Rigidbody2D playerRigidbody;
    private Transform playerTransform;

    [HideInInspector] public int enemyType;
    private float range;
    private float distance;

    // Start is called before the first frame update
    private void Start()
    {
        // Sets component variables
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();


        // Sets the enemy type
        enemyType = Random.Range(0,3);
        // Sets the enemy range
        // Eventually each enemy-type will have their own specific range (melee/range)
        range = Random.Range(1, 3);
        distance = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        distance = Vector2.Distance(myTransform.position, playerTransform.position);
        if (distance > range)
        {
            myTransform.position = Vector2.Lerp(myTransform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
