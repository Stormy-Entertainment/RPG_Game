using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeithSimpleMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float rotateSpeed;
    public bool isJumping;

    public float gravity;
    public float jump;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGrounded;

    //float turnSmoothVelocity;
    Vector3 velocity;

    void Update()
    {
        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //A,D rotate
        float rotation = Input.GetAxisRaw("Horizontal") * rotateSpeed;

        //W,S move forward and backward
        float moveFB = Input.GetAxisRaw("Vertical") * speed;

        moveFB *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, moveFB);
        transform.Rotate(0, rotation, 0);


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }
        if (Input.GetKeyDown("space") && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        print("jump");
        velocity.y += Mathf.Sqrt(jump * -2f * -gravity);
        isJumping = true;

    }

}
