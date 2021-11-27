using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyMove
{
    public float ProjectileDamage;
    private float ProjectileSpeed;
    private float ShootDuration;
    private float MaxCooldown;
    private float ShootCooldown;
    private bool atk;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        ProjectileDamage = 5f;
        ShootDuration = 0.5f;
        ShootCooldown = 0f;
        MaxCooldown = 2f;
        atk = false;
    }


    void FixedUpdate()
    {
        if (!IsActive) return;
    }


    void Update()
    {
        // If enemy is inside AttackRange (+ a small increment)
        if (enemy.distanceToPlayer <= enemy.AttackRange + 0.1f && ShootCooldown >= MaxCooldown)
        {
            TryStartMove();
        }

        if (IsActive)
        {
            if (Time.time > LastStartTime)
            {
                // Play Animation / Partickes
            }

            if (Time.time > LastStartTime + ShootDuration)
                EndMove();
        }
        ShootCooldown = ShootCooldown + (1 * Time.deltaTime);
    }


    public override void OnStartMove()
    {
        ShootCooldown = 0f;
        atk = true;

    }

    public override void OnEndMove()
    {

    }
}
