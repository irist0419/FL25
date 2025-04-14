using UnityEngine;

using System.Collections;
using System.Collections.Generic;
public class EnemyHealth : MonoBehaviour
{
    public AudioSource deathsound;

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
            // death sound
            //deathsound.enabled = true;
            //deathsound.Play();
        }

        if (currentHealth <= 0)
        {
            Die();

            if (deathsound != null)
            {
                // death sound
                deathsound.enabled = true;
                deathsound.Play();
            }
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
        float delay = deathsound != null ? deathsound.clip.length : 0.5f;
        Destroy(gameObject, delay);  // Destroy the enemy object
    }
}