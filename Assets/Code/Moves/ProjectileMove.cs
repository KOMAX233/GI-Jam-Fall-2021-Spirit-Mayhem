using UnityEngine;

public class ProjectileMove : Move
{
    public Rigidbody2D projectilePrefab;

    public float speed;
    public float windup;

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryStartMove();
        }

        if (IsActive && Time.time > LastStartTime + windup)
        {
            var projectile = Instantiate(projectilePrefab);
            var toMouse = player.MousePos - player.transform.position;
            projectile.transform.position = player.transform.position;
            projectile.velocity = speed * toMouse.normalized;
            EndMove();
        }
    }
}