using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float movmentSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;


    Vector2 movement;

    public bool mapView = true;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchPlane();
        }
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
            movement.x = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("Horizontal", movement.x);

            if (movement.x == 1f || movement.x == -1f)
            {
                animator.SetFloat("LastFacingX", movement.x);
            }

        }
        

    }

    public void SwitchPlane()
    {
        if (mapView)
        {
            mapView = false;
            rb.gravityScale = 5;
        }
        else
        {
            mapView = true;
            rb.gravityScale = 0;
        }
    }

    private void FixedUpdate()
    {
        
        
            rb.MovePosition(rb.position + movement * movmentSpeed * Time.fixedDeltaTime);
        
            
    }
}
