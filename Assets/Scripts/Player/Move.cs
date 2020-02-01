using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    public float moveSpeed = 2f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");       
        animator.SetFloat("x_input", movement.x);
        animator.SetFloat("y_input", movement.y);
        animator.SetFloat("speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
