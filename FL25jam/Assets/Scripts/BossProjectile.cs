using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 50f;
    public float projectileSpeed = 5f;
    private HeartHealth heartHealth; 
    
    private Rigidbody2D rb;

    void Start()
    {
        heartHealth = FindObjectOfType<HeartHealth>();
        
        rb = GetComponent<Rigidbody2D>();
        
        // Rotate the projectile based on direction
        RotateProjectile();

        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifeTime);
    }

    void RotateProjectile()
    {
        // Calculate direction to player (from firePoint to player)
        Vector2 direction = rb.linearVelocity.normalized;
        
        // Calculate the angle in degrees to rotate the projectile
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Apply the rotation to the projectile (rotate Z axis)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (heartHealth != null)
            {
                //Debug.Log("dealing damage to player");
                heartHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the projectile when hitting the player
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject); // Destroy the projectile when it hits anything else
        }
    }
}