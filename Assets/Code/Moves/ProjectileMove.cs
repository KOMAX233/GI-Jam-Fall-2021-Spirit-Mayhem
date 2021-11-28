using UnityEngine;

public class ProjectileMove : Move
{
    public int castButton = 1;
    public GameObject castIndicator;
    public Projectile projectilePrefab;
    public SpellParams spellParams;
    public SpellEffect spellEffect;

    public void Generate()
    {
        spellParams = SpellParams.Generate();
        spellParams.projectilePrefab = projectilePrefab;
        spellEffect = SpellEffect.Generate();

        var power = spellEffect.Power();
        power *= 1 - spellParams.windup;
        cooldown = .01f * power;
    }

    private void Start()
    {
        var castSprite = castIndicator.GetComponentInChildren<SpriteRenderer>();
        var newColor = spellParams.color;
        newColor.a = castSprite.color.a;
        castSprite.color = newColor;
    }

    private void Update()
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

        castIndicator.gameObject.SetActive(IsActive);
        if (IsActive)
        {
            var progress = Mathf.Clamp01((Time.time - LastStartTime) / spellParams.windup);
            castIndicator.transform.position = Player.transform.position;
            castIndicator.transform.localScale = (1 - progress) * Vector3.one;
        }
    }
}