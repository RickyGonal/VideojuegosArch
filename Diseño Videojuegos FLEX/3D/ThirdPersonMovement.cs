using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdPersonMovement : MonoBehaviour
{
    PlayerControlls controls;

    public CharacterController controller;
    public Transform cam;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    float speed;
    bool isSprinting;
    bool isCrouching;

    private Animator animator;

    Vector3 normalHeight;
    Vector3 crouchingHeight;

    float walkingSpeed = 2f;
    float SprintSpeed = 8f;
    float crouchSpeed = 3f;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector2 move;

    Vector3 velocity;
    bool isGrounded;

    int jumpCounter;
    float speedMovement;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = walkingSpeed;
        jumpCounter = 0;
        normalHeight = new Vector3(1f, 1f, 1f);
        crouchingHeight =  new Vector3(1f, 0.5f, 1f);
    }

    private void Awake()
    {
        controls = new PlayerControlls();
        controls.GamePlay.Jump.performed += ctx => Jump();
        controls.GamePlay.Sprint.performed += ctx => speed = SprintSpeed;
        controls.GamePlay.Sprint.canceled += ctx => speed = walkingSpeed;

        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCounter = 0;
        }

        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;

        /*
        if (Input.GetButtonDown("Crouch") && isGrounded)
        {
            isCrouching ^= true;
            if (isCrouching)
            {
                speed = crouchSpeed;
                transform.localScale = crouchingHeight;
            }
            else if (!isCrouching)
            {
                unCrouch();
            }
        }*/


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        speedMovement = speed * move.magnitude;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speedMovement * Time.deltaTime);
        }

        UpdateAnimation();
    }

    private void Jump()
    {
        if (!isCrouching)
        {
            if (jumpCounter <= 1)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCounter++;
            }
        }
        else
        {
            isCrouching = false;
            unCrouch();
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", speedMovement);
    }

    void unCrouch()
    {
        speed = walkingSpeed;
        transform.localScale = normalHeight;
    }
}
