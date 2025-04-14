using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class PlayerAttack : MonoBehaviour
{
    public AudioSource slash;

    [SerializeField] private float attackRange = 3f;  // The range at which the player can attack
    [SerializeField] private int attackDamage = 1;     // The damage the player does
    [SerializeField] private float attackCooldown = 1f; // Cooldown between attacks
    [SerializeField] private LayerMask enemyLayer;
    private float attackCooldownTimer = 0f;
    public static bool hasAttacked;

    private void Start()
    {
        hasAttacked = false;
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))  // Left mouse click
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Cast a ray directly at the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
            if (hit.collider != null)
            {
                // Check if we hit an enemy
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Slashing sound
                    slash.enabled = true;
                    slash.Play();
                    // Check if the enemy is within attack range
                    float distanceToEnemy = Vector2.Distance(transform.position, hit.collider.transform.position);
                    if (distanceToEnemy <= attackRange)
                    {
                        CursorControllerComplex.Instance.SetToMode(CursorControllerComplex.ModeOfCursor.Attack);

                        if (attackCooldownTimer <= 0f)
                        {
                            // If we're off cooldown and within range, process the attack
                            Debug.Log("Enemy hit at: " + hit.point + " | Distance: " + distanceToEnemy);
                            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                            if (enemyHealth != null)
                            {
                                enemyHealth.TakeDamage(attackDamage);
                                hasAttacked = true;
                            }
                            attackCooldownTimer = attackCooldown;  // Reset cooldown after attack
                        }
                    }
                    else
                    {
                        CursorControllerComplex.Instance.SetToMode(CursorControllerComplex.ModeOfCursor.Default);
                    }
                }
            }
        }
    }

    // Optional: visualize the attack range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}