using UnityEngine;

public class Move : MonoBehaviour
{
    public Player player;
    public float cooldown;
    public float LastStartTime { get; private set; }
    public bool IsActive { get; private set; }

    public void OnDisable()
    {
        player.allMoves.Add(this);
    }

    public void OnEnable()
    {
        player.allMoves.Add(this);
    }

    public bool TryStartMove()
    {
        if (IsActive || !(Time.time > LastStartTime + cooldown)) return false;
        LastStartTime = Time.time;
        IsActive = true;
        OnStartMove();
        return true;
    }

    public bool EndMove()
    {
        if (!IsActive) return false;
        IsActive = false;
        OnEndMove();
        return true;
    }

    public virtual void OnStartMove()
    {
    }

    public virtual void OnEndMove()
    {
    }
}