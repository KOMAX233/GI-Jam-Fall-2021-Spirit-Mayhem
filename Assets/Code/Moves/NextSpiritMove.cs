using UnityEngine;

public class NextSpiritMove : Move
{
    public float windup = .5f;

    private void Update()
    {
        if (Player.Instance.allSpirits.Count > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            TryStartMove();
        }

        if (IsActive && Time.time > LastStartTime + windup)
        {
            Player.Instance.allSpirits.RemoveAt(0);
            EndMove();
        }
    }
}