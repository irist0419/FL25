using UnityEngine;

public class LRBoss : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float attackRange = 10f;
    
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 6f;

    private float fireCooldown = 0f;
    private Rigidbody2D erb;
    private Transform player;
    
    // Player attack timer variables
    private float playerInactivityTimer = 0f;
    [SerializeField] private float maxInactivityTime = 60f; 
    private bool canAttack = true;
    private bool timerStarted = false;
    private bool playerHasBeenInRange = false;

    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        
        // Check if player has entered range for the first time and HasAttacked is false
        if (!playerHasBeenInRange && distance <= attackRange && !PlayerAttack.hasAttacked)
        {
            playerHasBeenInRange = true;
            timerStarted = true;
            Debug.Log("Player in range for first time. Starting 1-minute timer.");
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
        
        // If player has attacked at any point, stop the timer and leave canAttack as is
        if (PlayerAttack.hasAttacked && timerStarted)
        {
            timerStarted = false;
            Debug.Log("Player has attacked. Timer stopped at: " + playerInactivityTimer);
        }

        // Only fire if we're allowed to attack based on timer logic
        if (distance <= attackRange && fireCooldown <= 0f && canAttack)
        {
            Debug.Log("Firing!");
            Fire();
            fireCooldown = fireRate;
        }
    }

    void Fire()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
    }
    
    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            Vector2 moveDir = direction.normalized;
            erb.MovePosition(erb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    
}