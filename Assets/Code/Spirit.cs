using UnityEngine;

public class Spirit : MonoBehaviour
{
    public ProjectileMove MyProjectileMove { get; private set; }
    public SpriteRenderer MySpriteRenderer { get; private set; }

    public float chaseStartRange = .5f;
    public float chaseRate = .5f;
    public float goalDistance = .5f;

    private bool chasingPlayer;

    private void Start()
    {
        MyProjectileMove = GetComponentInChildren<ProjectileMove>(true);
        MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        MyProjectileMove.stats = ProjectileMove.ProjectileStats.Generate();
        MySpriteRenderer.color = MyProjectileMove.stats.color;
    }

    private void Update()
    {
        var toPlayer = Player.Instance.transform.position - transform.position;
        if (!chasingPlayer)
        {
            if (toPlayer.magnitude < chaseStartRange)
            {
                chasingPlayer = true;
                Player.Instance.allSpirits.Add(this);
            }
        }
        else
        {
            var placeInLine = Player.Instance.allSpirits.IndexOf(this);
            if (placeInLine == -1)
            {
                Destroy(gameObject);
                return;
            }

            if (placeInLine == 0)
            {
                MyProjectileMove.gameObject.SetActive(true);
            }

            var followPos = placeInLine == 0
                ? Player.Instance.transform.position
                : Player.Instance.allSpirits[placeInLine - 1].transform.position;
            var toFollowPos = followPos - transform.position;
            var toGoalPos = toFollowPos - goalDistance * toFollowPos.normalized;
            transform.position += toGoalPos * (1 - Mathf.Exp(-Time.deltaTime * chaseRate));
        }
    }
}