using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator anim;

    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 3f;

    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool isGounded;
    private bool isJumping = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isGounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGounded)
        {
            if (!isJumping)
            {
                CharacterJump();
            }
        }

        ApplyGravity();
        //Applied Simulated Gravity

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetButton("Sprint") && isGounded)
            {
                //Increase Speed
                controller.Move(moveDirection.normalized * sprintSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(moveDirection.normalized * normalSpeed * Time.deltaTime);
            }
        }
        anim.SetFloat("InputDirection", direction.magnitude);
        anim.SetFloat("Velocity", controller.velocity.magnitude);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CharacterJump()
    {
        isJumping = true;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        anim.SetBool("Jump", true);
        StartCoroutine(ResetJumpAnim());
    }

    IEnumerator ResetJumpAnim()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Jump", false);
        isJumping = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
