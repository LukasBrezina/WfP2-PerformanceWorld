using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public LockCamera lockCamera;
    public CharacterController controller;
 
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
 
    Vector3 velocity;
 
    public bool isGrounded;
 

    void Update()
    {
        // only be able to move when not in measuring state
        if (lockCamera != null && !lockCamera.isLocked) {

            // a ball is created, that checks if the floor is touched, checked by layer
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
    
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
    
            Vector3 move = transform.right * x + transform.forward * z;
    
            controller.Move(move * speed * Time.deltaTime);

    
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = (float)Math.Sqrt(jumpHeight * -2f * gravity);
            }
    
            velocity.y += gravity * Time.deltaTime;
    
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
