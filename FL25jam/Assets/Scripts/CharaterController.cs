using System;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 input;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        
        input.Normalize();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
