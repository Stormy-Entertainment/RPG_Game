using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplemove : MonoBehaviour
{
    //Setup speed and gravity
    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -10f;

    //Groundcheck setup
    public Transform groundCheck;
    public float groundDistance = 0.0f;
    public LayerMask groundMask;

    public GameObject mouseLook;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Player movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



    }
}
