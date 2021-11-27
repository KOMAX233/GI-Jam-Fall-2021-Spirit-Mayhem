using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAtack : EnemyMove
{
    public CollisionTracker swipeCollider;
    public float damage;
    private float hitDuration;
    private float MaxCooldown;
    private float attackCooldown;
    private bool atk;
    void Start()
    {
        damage = 10f;
        hitDuration = 0.7f;
        attackCooldown = 0f;
        MaxCooldown = 2f;
        atk = false;
    }

    void FixedUpdate()
    {
        if (!IsActive) return;
        foreach (var other in swipeCollider.triggerStay)
        {
            if (other != null)
            {
                var health = other.GetComponentInParent<Health>();
                if (health != null && other.name == "Player" && atk)
                {
                    health.Damage(damage);
                    atk = false;
                }
            }
        }
    }

    public void Update()
    {
        // If enemy is inside AttackRange (+ a small increment)
        if (enemy.distanceToPlayer <= enemy.AttackRange + 0.1f && attackCooldown >= MaxCooldown)
        {
            TryStartMove();
        }

        if (IsActive)
        {
            if (Time.time > LastStartTime)
            {
                if (!swipeCollider.gameObject.activeSelf)
                {
                    swipeCollider.gameObject.SetActive(true);
                    foreach (var ps in swipeCollider.gameObject.GetComponentsInChildren<ParticleSystem>())
                    {
                        ps.Play();
                    }
                }
            }

            if (Time.time > LastStartTime + hitDuration)
                EndMove();
        }
        attackCooldown = attackCooldown + (1 * Time.deltaTime);
    }

    public override void OnStartMove()
    {
        attackCooldown = 0f;
        atk = true;
        var attackPos = enemy.PlayerPosition() - enemy.transform.position;
        var zAngle = Mathf.Atan2(attackPos.y, attackPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }

    public override void OnEndMove()
    {
        swipeCollider.gameObject.SetActive(false);
    }
}
