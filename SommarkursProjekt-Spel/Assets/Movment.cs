using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float movmentSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;


    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement.magnitude > 1f)
        {
            movement = movement.normalized;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * movmentSpeed * Time.fixedDeltaTime);
    }
}
