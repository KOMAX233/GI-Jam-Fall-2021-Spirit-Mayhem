using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D MyRigidbody { get; private set; }
    public Health MyHealth { get; private set; }
    public Camera MyCamera { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LastNonzeroMoveDirection { get; private set; } = Vector2.down;
    public GameObject Spirit1;
    public GameObject Spirit2;
    public List<Vector2> PositionList;
    public int distance = 20;
    [HideInInspector] public List<Move> allMoves = new();

    public Vector3 MousePos => MyCamera.ScreenToWorldPoint(Input.mousePosition);

    private void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();
        MyCamera = GetComponentInChildren<Camera>();
        Spirit1.GetComponent<Renderer>().material.color = randColor();
        Spirit2.GetComponent<Renderer>().material.color = randColor();
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

        // spirit following
        PositionList.Add(transform.position);
        if (PositionList.Count > distance) {
            PositionList.RemoveAt(0);
            Spirit1.transform.position = PositionList[0] + new Vector2(1,0);
            Spirit2.transform.position = PositionList[0] + new Vector2(-1,0);
        }
    }

    private Color randColor() {
        Color genColor = new Color(Random.Range(0,255)/255f, Random.Range(0,255)/255f, Random.Range(0,255)/255f);
        return genColor;
    }
}