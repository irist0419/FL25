using UnityEngine;

using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;  // Maximum health of the enemy
    private int currentHealth;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private float flashDuration = 0.1f;

    void Start()
    {
        currentHealth = maxHealth;  // Set initial health
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Reduce health by damage amount
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        // Handle enemy death
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);  // Destroy the enemy object
    }
}