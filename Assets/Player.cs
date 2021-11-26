using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D MyRigidbody { get; private set; }
    public Health MyHealth { get; private set; }
    public Camera MyCamera { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LastNonzeroMoveDirection { get; private set; } = Vector2.down;

    [HideInInspector] public List<Move> allMoves = new();

    public Vector3 MousePos => MyCamera.ScreenToWorldPoint(Input.mousePosition);

    private void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();
        MyCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        MoveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            MoveDirection += Vector2.up;
        if (Input.GetKey(KeyCode.A))
            MoveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.S))
            MoveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.D))
            MoveDirection += Vector2.right;
        if (MoveDirection.sqrMagnitude > 0)
        {
            MoveDirection = MoveDirection.normalized;
            LastNonzeroMoveDirection = MoveDirection;
        }
    }
}