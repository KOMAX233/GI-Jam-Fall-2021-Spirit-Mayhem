using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float smoothing = 0.5f;

    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector2.up;
        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector2.right;
        if (moveDirection.sqrMagnitude > 1)
            moveDirection = moveDirection.normalized;

        var goalVelocity = moveDirection * speed;
        myRigidbody.velocity = Vector2.Lerp(goalVelocity, myRigidbody.velocity, smoothing);
    }
}