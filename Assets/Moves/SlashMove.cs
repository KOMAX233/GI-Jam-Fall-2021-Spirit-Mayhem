using System;
using UnityEngine;

public class SlashMove : Move
{
    [Serializable]
    public struct ComboPart
    {
        public CollisionTracker slashCollider;
        public float damage;
        public float windup;
        public float hitDuration;
        public float nextComboDuration;
    }

    public ComboPart[] comboParts;
    private int nextMove;
    private bool bufferedClick;

    private ComboPart CurrentPart => comboParts[nextMove];

    private void FixedUpdate()
    {
        if (!IsActive) return;
        foreach (var other in CurrentPart.slashCollider.triggerStay)
        {
            var health = other.GetComponentInParent<Health>();
            if (health != null && health != player.MyHealth)
            {
                health.Damage(CurrentPart.damage);
            }
        }

        CurrentPart.slashCollider.triggerStay.Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || bufferedClick)
        {
            bufferedClick = !TryStartMove();
        }

        if (IsActive)
        {
            if (Time.time > LastStartTime + CurrentPart.windup)
                CurrentPart.slashCollider.gameObject.SetActive(true);
            if (Time.time > LastStartTime + CurrentPart.windup + CurrentPart.hitDuration)
                EndMove();
        }

        if (Time.time > LastStartTime + CurrentPart.windup + CurrentPart.hitDuration + CurrentPart.nextComboDuration)
        {
            nextMove = 0;
        }
    }

    public override void OnStartMove()
    {
        var toMouse = player.MousePos - player.transform.position;
        var zAngle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);

        CurrentPart.slashCollider.triggerStay.Clear();
    }

    public override void OnEndMove()
    {
        CurrentPart.slashCollider.gameObject.SetActive(false);
        nextMove = (nextMove + 1) % comboParts.Length;
    }
}