using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    [HideInInspector] public List<Collider2D> triggerStay = new();

    private void OnTriggerStay2D(Collider2D other)
    {
        triggerStay.Add(other);
    }
}