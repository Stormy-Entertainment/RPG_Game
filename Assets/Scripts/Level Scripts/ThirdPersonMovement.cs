using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private Transform cam;
    [SerializeField] private Animator anim;

    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 3f;

    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector2 velocity;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool isGounded;
    private bool isJumping = false;

    float horizontal = 0f;
    float vertical = 0f;

    bool Rotate = false;
    bool OverrideRotation = false;

    //new, move forward, when player stand on top of the enemy
    [SerializeField] private bool isEnemy;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private Footstepper leftFootStep;
    [SerializeField] private Footstepper rightFootStep;
    bool footStepPlayer = false;
    bool leftFootPlayed = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        //new, move forward, when player stand on top of the enemy
        isEnemy = Physics.CheckSphere(groundCheck.position, groundDistance, enemyMask);
        if (isEnemy)
        {
            Vector3 move = transform.forward * 1;
            controller.Move(move * Time.deltaTime * 30);
        }

        //Check Grounded
        isGounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If Jump Button Pressed
        if (Input.GetButtonDown("Jump") && isGounded)
        {
            if (!isJumping)
            {
                CharacterJump();
            }
        }

        if (isGounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        ApplyGravity();
        //Applied Simulated Gravity

        if (Rotate)
        {
            ApplyRotation();
            //Simulated Rotation according to Camera
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (cam != null)
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Rotate = true;

                if (Input.GetButton("Sprint") && isGounded)
                {
                    //Increase Speed
                    controller.Move(moveDirection.normalized * sprintSpeed * Time.deltaTime);
                    velocity.x += sprintSpeed * Time.deltaTime;
                }
                else
                {
                    controller.Move(moveDirection.normalized * normalSpeed * Time.deltaTime);
                    velocity.x += normalSpeed * Time.deltaTime;
                }
            }
            else
            {
                velocity.x = 0f;
                if (!OverrideRotation)
                {
                    Rotate = false;
                }
            }
        }

        //Play foot step when is grounded and moving
        if (isGounded && direction.magnitude >= 0.1f)
        {
            if (!footStepPlayer)
            {
                if (Input.GetButton("Sprint"))
                {
                    StartCoroutine(PlayFootStepsRunning());
                }
                else
                {   
                    StartCoroutine(PlayFootStepsWalking());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Velocity", controller.velocity.magnitude);
        anim.SetFloat("H", horizontal);
        anim.SetFloat("V", vertical);
        // Debug.Log("Velovity " + velocity);
    }

    IEnumerator PlayFootStepsWalking()
    {
        footStepPlayer = true;
        yield return new WaitForSeconds(0.5f);
        if (!leftFootPlayed && controller.velocity.magnitude >= 0.1f) 
        {
            leftFootStep.PlayFootStep();
            leftFootPlayed = true;
        }
        else if(controller.velocity.magnitude >= 0.1f)
        {
            rightFootStep.PlayFootStep();
            leftFootPlayed = false;
        }
        footStepPlayer = false;
    }

    IEnumerator PlayFootStepsRunning()
    {
        footStepPlayer = true;
        yield return new WaitForSeconds(0.3f);
        if (!leftFootPlayed && controller.velocity.magnitude >= 0.1f)
        {
            leftFootStep.PlayFootStep();
            leftFootPlayed = true;
        }
        else if (controller.velocity.magnitude >= 0.1f)
        {
            rightFootStep.PlayFootStep();
            leftFootPlayed = false;
        }
        footStepPlayer = false;
    }

    public void UpdateRotation(bool value)
    {
        Rotate = value;
        OverrideRotation = value;
    }

    private void ApplyRotation()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(new Vector2(0f, velocity.y) * Time.deltaTime);
    }

    private void CharacterJump()
    {
        isJumping = true;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        anim.SetTrigger("Jump");
        SFXManager.GetInstance().PlaySound("Jump");
        StartCoroutine(ResetJumpAnim());
    }

    IEnumerator ResetJumpAnim()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
