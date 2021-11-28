using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMeleeAtack : EnemyMove
{
    [Serializable]
    public class EnemyProjectileStats
    {
        public float damage;
        public float range;
        public float speed;
        public float cooldown;
        public float windup;
        public float size;
        public Color color;

        public static EnemyProjectileStats Generate()
        {
            var s = new EnemyProjectileStats
            {
                damage = Random.Range(10, 15),
                range = 1,
                speed = Random.Range(3, 8),
                windup = Random.Range(0.5f, 1f),
                size = Random.Range(.5f, 1f),
                color = new Color(Random.value, Random.value, Random.value, .5f + .5f * Random.value)
            };
            var power = s.damage * (1 + s.range * s.speed * s.size / 1000) * (1 - s.windup);
            s.cooldown = .02f * power;
            return s;
        }
    }

    public Rigidbody2D projectilePrefab;

    public bool randomizeStats = true;
    public EnemyProjectileStats stats;

    public void Start()
    {
        if (randomizeStats)
        {
            stats = EnemyProjectileStats.Generate();
        }

        cooldown = stats.cooldown;
    }

    public void Update()
    {
        if (enemy.distanceToPlayer <= enemy.MAttackRange + 0.1f)
        {
            TryStartMove();
        }

        if (IsActive && Time.time > LastStartTime + stats.windup)
        {
            var projectile = Instantiate(projectilePrefab);
            var projectileComponent = projectile.GetComponent<Projectile>();
            var spriteComponent = projectile.GetComponentInChildren<SpriteRenderer>();
            var attackPos = enemy.PlayerPosition() - enemy.transform.position;
            attackPos.z = 0;

            projectile.transform.position = enemy.transform.position;
            projectile.transform.Rotate(0, 0, Mathf.Atan2(attackPos.y, attackPos.x) * Mathf.Rad2Deg);
            projectile.transform.localScale = new Vector3(stats.size, stats.size, stats.size);
            projectile.velocity = stats.speed * attackPos.normalized;
            projectileComponent.damage = stats.damage;
            spriteComponent.color = stats.color;

            var lifetime = stats.range / stats.speed;
            Destroy(projectile.gameObject, lifetime);

            EndMove();
        }
    }
}
