using UnityEngine;

public class WalkMove : Move
{
    public float speed = 4;
    public float smoothing = .5f;

    private void FixedUpdate()
    {
        if (!IsActive) return;
        var goalVelocity = player.MoveDirection * speed;
        player.MyRigidbody.velocity = Vector2.Lerp(goalVelocity, player.MyRigidbody.velocity, smoothing);
    }

    private void Update()
    {
        if (player.allMoves.Exists(m => m != this && m.IsActive))
        {
            EndMove();
        }
        else
        {
            TryStartMove();
        }
    }
}