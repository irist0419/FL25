using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;  // Maximum health of the enemy
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;  // Set initial health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Reduce health by damage amount
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);  // Destroy the enemy object
    }
}