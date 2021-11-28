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
            Destroy(gameObject, 1);
        }
    }
    public void Update()
    {
        currentHealth = maxHealth - damageTaken;
        if (currentHealth <= 0) { 
            Destroy(gameObject, 1); 
        }
    }
}