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
    
    

    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime; // ← this goes at the top now

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && fireCooldown <= 0f)
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
            //Debug.Log("Moving toward player.");
        }
    }


}