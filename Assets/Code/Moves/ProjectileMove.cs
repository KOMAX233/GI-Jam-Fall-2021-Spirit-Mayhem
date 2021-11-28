using UnityEngine;

public class ProjectileMove : Move
{
    public int castButton = 1;
    public Projectile projectilePrefab;
    public SpellParams spellParams;
    public SpellEffect spellEffect;

    public void Generate()
    {
        spellParams = SpellParams.Generate();
        spellEffect = SpellEffect.Generate();

        var power = spellEffect.Power();
        power *= 1 - spellParams.windup;
        cooldown = .01f * power;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(castButton))
        {
            TryStartMove();
        }

        if (IsActive && Time.time > LastStartTime + spellParams.windup)
        {
            var toMouse = (Vector2) (Player.MousePos - Player.transform.position);
            toMouse = toMouse.normalized;

            var projectile = Instantiate(projectilePrefab);
            projectile.Cast(spellParams, spellEffect, Player.transform.position, toMouse);

            EndMove();
        }
    }
}