using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float movmentSpeed = 5f;
    
    public float jumpForce = 17f;

    public Rigidbody2D rb;

    public Animator animator;


    Vector2 movement;

    public bool mapView = false;

    [SerializeField] Transform groundCheck;


    [SerializeField] private LayerMask groundLayer;

    public bool canjump = false;


    // Update is called once per frame
    void Update()
    {

        
        if (mapView)
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

            if(movement.x == 1f || movement.x == -1f || movement.y == 1f || movement.y == -1f)
            {
                animator.SetFloat("LastFacingX", movement.x);
                animator.SetFloat("LastFacingY", movement.y);
            }
        }
        else
        {
            if (canjump)
            {

                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            movement.x = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.x == 1f || movement.x == -1f)
            {
                animator.SetFloat("LastFacingX", movement.x);
            }

        }
        

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    public void HasTrousers()
    {
        canjump = true;
    }

    public void SwitchPlane()
    {
        if (mapView)
        {
            mapView = false;
            rb.gravityScale = 4;
        }
        else
        {
            mapView = true;
            rb.gravityScale = 0;
        }
    }

    private void FixedUpdate()
    {

        if (mapView)
        {

            rb.MovePosition(rb.position + movement * movmentSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = new Vector2(movement.x * movmentSpeed, rb.velocity.y);
        }
        
            
    }
}
