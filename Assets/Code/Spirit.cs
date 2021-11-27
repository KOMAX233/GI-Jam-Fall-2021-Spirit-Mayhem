using UnityEngine;

public class Spirit : MonoBehaviour
{
    public ProjectileMove MyProjectileMove { get; private set; }
    public SpriteRenderer MySpriteRenderer { get; private set; }

    public float chaseStartRange = .5f;
    public float chaseRate = .5f;

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
                MyProjectileMove.gameObject.SetActive(true);
                chasingPlayer = true;
            }
        }
        else
        {
            transform.position += toPlayer * (1 - Mathf.Exp(-Time.deltaTime * chaseRate));
        }
    }
}