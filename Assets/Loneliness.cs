using UnityEngine;

public class Loneliness : MonoBehaviour
{
    public float damageRate = 5;
    public accompanimentBar accompanimentbar;

    private void Start()
    {
        accompanimentbar.setInitialAccompaniment(0);
        accompanimentbar.setMaxAccompaniment(Player.Instance.MyHealth.maxHealth);
    }

    private void Update()
    {
        Player.Instance.MyHealth.Damage(damageRate * Time.deltaTime * (1 - Player.Instance.allSpirits.Count));
        accompanimentbar.setAccompaniment(Player.Instance.MyHealth.damageTaken);
    }
}