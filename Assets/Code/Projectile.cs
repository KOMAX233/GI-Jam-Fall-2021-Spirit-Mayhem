using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.rigidbody.GetComponentInParent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }

        Destroy(gameObject);
    }
}