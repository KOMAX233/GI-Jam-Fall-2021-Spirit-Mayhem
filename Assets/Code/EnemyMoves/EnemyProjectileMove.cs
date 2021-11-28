using UnityEngine;

public class EnemyProjectileMove : EnemyMove
{
    public Projectile projectilePrefab;
    public SpellParams spellParams;
    public SpellEffect spellEffect;

    public void Start()
    {
        spellParams = SpellParams.Generate();
        spellEffect = SpellEffect.Generate();

        var power = spellEffect.Power();
        power *= 1 - spellParams.windup;
        cooldown = .02f * power;
    }

    public void Update()
    {
        if (enemy.distanceToPlayer <= enemy.RAttackRange + 0.1f)
        {
            // Attack if ranged enemy, or if boss and outside of melee range
            if (enemy.type == 1 || enemy.distanceToPlayer > enemy.MAlertRange)
            {
                TryStartMove();
            }
        }

        if (IsActive && Time.time > LastStartTime + spellParams.windup)
        {
            var toPlayer = (Vector2) (enemy.PlayerPosition() - enemy.transform.position);
            toPlayer = toPlayer.normalized;

            var projectile = Instantiate(projectilePrefab);
            projectile.Cast(spellParams, spellEffect, transform.position, toPlayer);

            EndMove();
        }
    }
}