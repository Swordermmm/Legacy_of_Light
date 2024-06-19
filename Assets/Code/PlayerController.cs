using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction;
    public bool canMove = true;

    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }
}
