using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float smoothing = 0.5f;

    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector3.right;
        if (moveDirection.sqrMagnitude > 1)
            moveDirection = moveDirection.normalized;

        var goalVelocity = moveDirection * speed + myRigidbody.velocity.y * Vector3.up;
        myRigidbody.velocity = Vector3.Lerp(goalVelocity, myRigidbody.velocity, smoothing);
    }
}