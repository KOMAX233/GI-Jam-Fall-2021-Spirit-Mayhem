using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D MyRigidbody { get; private set; }
    public SpriteRenderer MySpriteRenderer { get; private set; }

    private SpellParams sp;
    private SpellEffect se;
    private Vector2 spawnPos;
    private Vector2 spawnDir;
    private float spawnTime;
    private HashSet<Health> hit = new();
    private bool onReturn;

    private void Awake()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Cast(SpellParams spellParams, SpellEffect spellEffect, Vector2 pos, Vector2 dir)
    {
        sp = spellParams;
        se = spellEffect;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += Random.Range(-1f, 1f) * se.directionalNoise;
        var rot = Quaternion.Euler(0, 0, angle);
        dir = rot * Vector3.right;

        transform.position = pos;
        transform.rotation = rot;
        transform.localScale = (Vector3) se.size + Vector3.forward;
        MyRigidbody.velocity = se.speed * dir;

        MySpriteRenderer.color = sp.color;

        spawnPos = pos;
        spawnDir = dir;
        spawnTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponentInParent<Health>();
        if (health != null && !hit.Contains(health))
        {
            health.Damage(se.damage);
            hit.Add(health);

            var dir = (Vector2) health.transform.position - spawnPos;
            dir = dir.normalized;
            foreach (var se2 in se.onHit)
            {
                var projectile = Instantiate(sp.projectilePrefab);
                projectile.Cast(sp, se2, health.transform.position, dir);
            }
        }

        if (!se.pierce || hit.Count >= 2) TryDestroySelf();
    }

    private void TryDestroySelf()
    {
        if (se.returning && !onReturn)
        {
            onReturn = true;
            MyRigidbody.velocity *= -1;
            hit.Clear();
            spawnTime = Time.time - se.lifetime + (Time.time - spawnTime);
            return;
        }

        Destroy(gameObject);

        if (hit.Count != 0) return;
        var dir = (Vector2) transform.position - spawnPos;
        dir += .1f * spawnDir;
        dir = dir.normalized;
        foreach (var se2 in se.onHit)
        {
            var projectile = Instantiate(sp.projectilePrefab);
            projectile.Cast(sp, se2, transform.position, dir);
        }
    }

    private void Update()
    {
        var lifeState = (Time.time - spawnTime) / se.lifetime;
        if (lifeState > 1) TryDestroySelf();

        if (se.growOverTime)
        {
            if (onReturn) lifeState = 1 - lifeState;
            transform.localScale = lifeState * (Vector3) se.size + Vector3.forward;
        }
    }
}