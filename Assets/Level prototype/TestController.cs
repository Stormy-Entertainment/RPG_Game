using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    private CharacterController _controller;

    public float rotateSpeed;

    public float speed;
    public float jump;
    public float gravity;
    public Transform body;

    public LayerMask Ground;
    public Transform _groundCheck;
    public bool _isGrounded = true;

    Vector3 _velocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * z;
        _controller.Move(move * Time.deltaTime * speed);


        float rotation = Input.GetAxisRaw("Horizontal") * rotateSpeed;
        body.Rotate(Vector3.up * rotation);

        _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.1f, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetKey("space") && _isGrounded)
        {
            print("jump");
            _velocity.y += jump;
        }



    }
}
