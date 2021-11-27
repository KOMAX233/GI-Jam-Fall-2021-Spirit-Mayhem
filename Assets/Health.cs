using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;
    private float damageTaken;
    public float currentHealth;

    public void Damage(float damage)
    {
        damageTaken += damage;
        if (damageTaken >= maxHealth)
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        currentHealth = maxHealth - damageTaken;
    }
}