using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;  // The range at which the player can attack
    [SerializeField] private int attackDamage = 1;     // The damage the player does
    [SerializeField] private float attackCooldown = 1f; // Cooldown between attacks
    [SerializeField] private LayerMask enemyLayer;
    private float attackCooldownTimer = 0f;
    

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))  // Left mouse click
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Cast a ray directly at the mouse position (same as NPCcontrol)
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
            if (hit.collider != null)
            {
                // Check if we hit an enemy
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (attackCooldownTimer <= 0f)
                    {
                        // If we're off cooldown, process the attack
                        Debug.Log("Enemy hit at: " + hit.point);
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(attackDamage);
                        }
                        attackCooldownTimer = attackCooldown;  // Reset cooldown after attack
                    }
                }
            }
            
            // Debug visualization
            Debug.DrawRay(mousePos, Vector3.forward, Color.red, 1f);
        }
    }
}