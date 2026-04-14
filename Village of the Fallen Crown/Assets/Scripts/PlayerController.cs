using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float crouchSpeed = 3f;

    [Header("Jump")]
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    [Header("Crouch")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;
    public Transform playerCamera;
    public float standingCameraY = 1.6f;
    public float crouchCameraY = 0.9f;
    public float crouchTransitionSpeed = 10f;

    [Header("State")]
    public bool canMove = true;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        controller.height = standingHeight;
        controller.center = new Vector3(0, standingHeight / 2f, 0);

        if (playerCamera != null)
        {
            Vector3 camPos = playerCamera.localPosition;
            camPos.y = standingCameraY;
            playerCamera.localPosition = camPos;
        }
    }

    void Update()
    {
        if (!canMove) return;

        HandleGroundCheck();
        HandleCrouch();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        UpdateCameraHeight();
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            controller.center = new Vector3(0, crouchHeight / 2f, 0);
        }
        else
        {
            isCrouching = false;
            controller.height = standingHeight;
            controller.center = new Vector3(0, standingHeight / 2f, 0);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateCameraHeight()
    {
        if (playerCamera == null) return;

        Vector3 camPos = playerCamera.localPosition;
        float targetY = isCrouching ? crouchCameraY : standingCameraY;
        camPos.y = Mathf.Lerp(camPos.y, targetY, crouchTransitionSpeed * Time.deltaTime);
        playerCamera.localPosition = camPos;
    }
}