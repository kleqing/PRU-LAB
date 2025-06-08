using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damage;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DestroyBullet();
        SetStraightVelocity();
    }
    
    private void SetStraightVelocity()
    {
        float playerDirection = Mathf.Sign(transform.lossyScale.x);
        rb.linearVelocity = transform.up * bulletSpeed * playerDirection;
    }
    
    private void DestroyBullet()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            // Damage the other object
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
