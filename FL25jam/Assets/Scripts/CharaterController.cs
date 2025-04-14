using System;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 input;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x < 0)
            spriteRenderer.flipX = true;
        else if (input.x > 0)
            spriteRenderer.flipX = false;
        
        
        input.Normalize();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
