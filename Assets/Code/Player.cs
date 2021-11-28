using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null) _instance = playerObject.GetComponent<Player>();
            return _instance;
        }
        private set => _instance = value;
    }

    public Rigidbody2D MyRigidbody { get; private set; }
    public Health MyHealth { get; private set; }
    public Camera MyCamera { get; private set; }
    public Animator MyAnimator { get; private set; }

    public Vector2 MoveDirection { get; private set; }
    public Vector2 LastNonzeroMoveDirection { get; private set; } = Vector2.down;

    public GameObject playerCamera;
    public GameObject worldCamera;
    [HideInInspector] public List<Move> allMoves = new();
    [HideInInspector] public List<Spirit> allSpirits = new();
    private static Player _instance;

    public Vector3 MousePos => MyCamera.ScreenToWorldPoint(Input.mousePosition);

    private void Awake()
    {
        Instance = this;
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyHealth = GetComponent<Health>();
        MyCamera = GetComponentInChildren<Camera>();
        MyAnimator = GetComponentInChildren<Animator>();
        worldCamera.SetActive(false);
        Time.timeScale = 1;
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

        MyAnimator.SetFloat("horizontal", LastNonzeroMoveDirection.x);
        MyAnimator.SetFloat("vertical", LastNonzeroMoveDirection.y);
        MyAnimator.SetFloat("speed", LastNonzeroMoveDirection.sqrMagnitude);
        MyAnimator.SetBool("attack", allMoves.Exists(m => m is ProjectileMove && m.IsActive));
    }

    public void OnPlayerDeath()
    {
        Time.timeScale = 0;
        worldCamera.SetActive(true);
        playerCamera.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}