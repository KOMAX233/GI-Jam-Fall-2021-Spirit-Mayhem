using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;
    public UnityEvent onDeath;

    public float damageTaken;

    public float CurrentHealth => maxHealth - damageTaken;

    public void Damage(float damage)
    {
        damageTaken += damage;
        if (damageTaken < 0) damageTaken = 0;
        if (damageTaken >= maxHealth)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}