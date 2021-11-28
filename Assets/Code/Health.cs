using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;
    public UnityEvent onDeath;

    private float damageTaken;

    public float CurrentHealth => maxHealth - damageTaken;

    public void Damage(float damage)
    {
        damageTaken += damage;
        if (damageTaken >= maxHealth)
        {
            onDeath.Invoke();
            Destroy(gameObject);
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