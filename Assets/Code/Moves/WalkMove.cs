using UnityEngine;

public class WalkMove : Move
{
    public float speed = 4;
    public float smoothing = .5f;

    private void FixedUpdate()
    {
        if (!IsActive) return;
        var goalVelocity = Player.MoveDirection * speed;
        Player.MyRigidbody.velocity = Vector2.Lerp(goalVelocity, Player.MyRigidbody.velocity, smoothing);
    }

    private void Update()
    {
        if (Player.allMoves.Exists(m => m != this && m.IsActive))
        {
            EndMove();
        }
        else
        {
            TryStartMove();
        }
    }
}