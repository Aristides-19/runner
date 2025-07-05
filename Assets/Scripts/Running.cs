using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerGroundController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpPower = 8f;
    public float gravity = -20f;
    public float rotationSmoothness = 10f;

    [Header("Ground Settings")]
    public LayerMask groundLayer;
    public float groundStickForce = 10f;
    public float groundCheckOffset = 0.1f;

    private CharacterController controller;
    private Transform cameraTransform;
    private float verticalVelocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Configuración inicial del CharacterController
        controller.center = new Vector3(0, controller.height / 2, 0);
        controller.skinWidth = 0.08f;


    }

    void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        StickToGround();
    }

    private void CheckGrounded()
    {
        float rayLength = controller.height / 2 + groundCheckOffset;
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            rayLength,
            groundLayer
        );
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        if (moveDirection != Vector3.zero)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSmoothness * Time.deltaTime
            );
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpPower * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -groundStickForce; // Fuerza para pegar al suelo
        }

        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void StickToGround()
    {
        if (isGrounded)
        {
            RaycastHit hit;
            float rayLength = controller.height / 2 + 0.5f;

            if (Physics.Raycast(
                transform.position,
                Vector3.down,
                out hit,
                rayLength,
                groundLayer))
            {
                float surfaceOffset = 0.05f;
                float targetHeight = hit.point.y + controller.height / 2 + surfaceOffset;

                // Suavizar el ajuste de altura
                float newY = Mathf.Lerp(
                    transform.position.y,
                    targetHeight,
                    10f * Time.deltaTime
                );

                transform.position = new Vector3(
                    transform.position.x,
                    newY,
                    transform.position.z
                );
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Ajuste adicional para pendientes pronunciadas
        if (hit.normal.y < 0.7f && verticalVelocity <= 0)
        {
            verticalVelocity = -10f;
        }
    }
}