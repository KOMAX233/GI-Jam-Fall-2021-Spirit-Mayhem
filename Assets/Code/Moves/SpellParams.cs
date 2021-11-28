using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpellParams
{
    public float windup;
    public Color color;

    public static SpellParams Generate()
    {
        return new SpellParams
        {
            windup = Random.Range(0, .5f),
            color = new Color(Random.value, Random.value, Random.value)
        };
    }
}