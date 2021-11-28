using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpellEffect
{
    public float directionalNoise;
    public float lifetime;
    public float speed;
    public Vector2 size;
    public bool pierce;
    public bool growOverTime;
    public bool returning;

    public float damage;
    public List<SpellEffect> onHit;
    public string description;

    private static SpellEffect GenerateAOE()
    {
        var s = new SpellEffect
        {
            lifetime = Random.Range(.1f, 1),
            speed = Random.value < .5f ? 0 : Random.Range(0, 10f),
            size = new Vector2(Random.Range(1.5f, 3.5f), Random.Range(1.5f, 3.5f)),
            pierce = true,
            growOverTime = Random.value < .8f,
            returning = Random.value < .2f,

            damage = Random.Range(1, 100f),
            onHit = new List<SpellEffect>(),
        };
        s.description =
            (s.returning ? "Returning " : "") +
            (s.speed > 0 ? "Cone " : "") +
            "AOE Attack";
        return s;
    }

    private static SpellEffect GenerateProjectile()
    {
        var range = Random.Range(3, 10f);
        var speed = Random.Range(5, 30f);
        var s = new SpellEffect
        {
            lifetime = range / speed,
            speed = speed,
            size = new Vector2(Random.Range(.5f, 1.5f), Random.Range(.5f, 1.5f)),
            pierce = Random.value < .2f,
            returning = Random.value < .2f,

            damage = Random.Range(1, 100f),
            onHit = new List<SpellEffect>(),
        };
        s.description =
            (s.pierce ? "Piercing " : "") +
            (s.returning ? "Returning " : "") +
            "Projectile Attack";
        return s;
    }

    public static SpellEffect Generate()
    {
        var s = Random.value < .3f ? GenerateAOE() : GenerateProjectile();

        if (s.damage > 50) s.description += ", High Damage";
        else if (s.damage > 25) s.description += ", Medium Damage";
        else s.description += ", Low Damage";

        while (Random.value < .3f)
        {
            var newSpellEffect = Generate();
            s.onHit.Add(newSpellEffect);
            s.description += "\nThen: " + newSpellEffect.description;
        }

        return s;
    }

    public float Power()
    {
        var power = damage + onHit.Sum(se => se.Power());
        power *= 1 + lifetime * speed / 1000 + (size.x + size.y) / 10f;
        if (pierce) power *= 2;
        if (growOverTime) power *= .5f;
        if (returning) power *= 1.5f;
        return power;
    }
}