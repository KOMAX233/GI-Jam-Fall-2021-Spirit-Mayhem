using UnityEngine;

public class EnemyProjectileMove : EnemyMove
{
    public GameObject castIndicator;
    public Projectile projectilePrefab;
    public SpellParams spellParams;
    public SpellEffect spellEffect;

    public void Start()
    {
        spellParams = SpellParams.Generate();
        spellParams.projectilePrefab = projectilePrefab;
        spellEffect = SpellEffect.Generate();

        var power = spellEffect.Power();
        power *= 1 - spellParams.windup;
        cooldown = .1f * power;
        spellParams.windup += .5f;

        var castSprite = castIndicator.GetComponentInChildren<SpriteRenderer>();
        var newColor = spellParams.color;
        newColor.a = castSprite.color.a;
        castSprite.color = newColor;
    }

    public void Update()
    {
        if (enemy.DistanceToPlayer <= enemy.RAttackRange + 0.1f)
        {
            TryStartMove();
        }

        if (IsActive && Time.time > LastStartTime + spellParams.windup)
        {
            var toPlayer = (Vector2) (enemy.PlayerPosition() - enemy.transform.position);
            toPlayer = toPlayer.normalized;

            var projectile = Instantiate(projectilePrefab);
            projectile.Cast(spellParams, spellEffect, transform.position, toPlayer);

            EndMove();
        }

        castIndicator.gameObject.SetActive(IsActive);
        if (IsActive)
        {
            var progress = Mathf.Clamp01((Time.time - LastStartTime) / spellParams.windup);
            castIndicator.transform.position = enemy.transform.position;
            castIndicator.transform.localScale = (1 - progress) * Vector3.one;
        }
    }
}