using System;
using UnityEngine;

public class SRBoss: MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;  
    [SerializeField] private int attackDamage = 1;    
    [SerializeField] private float attackCooldown = 1f;
    private float attackCooldownTimer = 0f;
    
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 6f;
    
    // Timer for player inactivity
    private float playerInactivityTimer = 0f;
    [SerializeField] float maxInactivityTime = 60f;
    private bool canAttack = true;
    private bool timerStarted = false;
    private bool playerHasBeenInRange = false;
    
    private Rigidbody2D erb;
    private Transform player;
    private HeartHealth heartHealth;
    
    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (player != null)
        {
            heartHealth = player.GetComponent<HeartHealth>();
            if (heartHealth == null)
            {
                heartHealth = player.GetComponentInChildren<HeartHealth>();
            }
        }
        
        if (heartHealth == null)
        {
            heartHealth = FindObjectOfType<HeartHealth>();
            Debug.Log("HeartHealth found: " + (heartHealth != null));
        }
    }

    private void Update()
    {
        if (player == null) return;
        
        float distance = Vector2.Distance(transform.position, player.position);
        
        // Check if player has entered range for the first time and HasAttacked is false
        if (!playerHasBeenInRange && distance <= attackRange && !PlayerAttack.hasAttacked)
        {
            playerHasBeenInRange = true;
            timerStarted = true;
            Debug.Log("Player in range for first time. Starting 1.5-minute timer.");
        }
        
        // If timer has started, increment it
        if (timerStarted)
        {
            playerInactivityTimer += Time.deltaTime;
            
            // If 3 minutes have passed since player first entered range
            if (playerInactivityTimer >= maxInactivityTime && canAttack)
            {
                canAttack = false;
                Debug.Log("1 minutes have passed since player entered range. Boss stopped attacking.");
            }
        }
        
        // If player has attacked at any point, stop the timer and set canAttack based on its state
        if (PlayerAttack.hasAttacked && timerStarted)
        {
            timerStarted = false;
            Debug.Log("Player has attacked. Timer stopped at: " + playerInactivityTimer);
        }
        
        // Decrease the cooldown timer
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        
        // Only attack if we're allowed to attack based on player activity
        if (distance <= attackRange && attackCooldownTimer <= 0f && canAttack)
        {
            Debug.Log("Enemy Attacking!");
            Attack();                         
            attackCooldownTimer = attackCooldown;  // Reset cooldown after attack
        }
    }
    
    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            erb.MovePosition(erb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    void Attack()
    {
        if (heartHealth != null)
        {
            Debug.Log("Dealing damage to player - Amount: " + attackDamage);
            heartHealth.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogError("HeartHealth component not found on player!");
        }
    }
    
    // Visual debug to see attack range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        if (Application.isPlaying)
        {
            // Show attack status
            Gizmos.color = canAttack ? Color.green : Color.gray;
            Gizmos.DrawWireSphere(transform.position, attackRange * 0.8f);
            
            // Show timer progress
            if (timerStarted)
            {
                float timerRatio = Mathf.Clamp01(playerInactivityTimer / maxInactivityTime);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, attackRange * 0.6f * timerRatio);
            }
        }
    }
}