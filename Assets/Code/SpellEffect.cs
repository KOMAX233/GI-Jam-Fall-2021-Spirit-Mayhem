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


    public static SpellEffect Generate()
    {
        var range = Random.Range(3, 10f);
        var speed = Random.Range(5, 30f);
        var s = new SpellEffect
        {
            lifetime = range / speed,
            speed = speed,
            size = new Vector2(Random.Range(.5f, 1.5f), Random.Range(.5f, 1.5f)),
            pierce = Random.value < .5f,
            returning = Random.value < .3f,

            damage = Random.Range(1, 100f),
            onHit = new List<SpellEffect>(),
        };
        while (Random.value < .5f)
        {
            s.onHit.Add(Generate());
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