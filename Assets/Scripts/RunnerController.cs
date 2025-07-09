using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Animator))]
public class RunnerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float forwardSpeed = 10f;

    [SerializeField]
    private float laneDistance = 3f;

    [SerializeField]
    private float laneChangeSpeed = 8f;

    [Space]
    [Header("Jump Settings")]
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private LayerMask groundLayer;

    [Space]
    [Header("Slide Settings")]
    [SerializeField]
    private float slideDuration = 1f;

    // Internal variables
    private int currentLane = 1; // 0: L, 1: C, 2: R
    private bool isGrounded;
    private bool isSliding = false;
    private float groundCheckDistance = 0.3f; // Minimum distance to check if grounded
    private Vector3 targetPosition;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        targetPosition = rb.position;

        // Initialize the capsule collider and its original properties
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 forwardMove = Vector3.forward * forwardSpeed * Time.fixedDeltaTime;
        Vector3 lateralTarget = new Vector3(targetPosition.x, rb.position.y, rb.position.z);
        Vector3 lateralMove = Vector3.Lerp(
            rb.position,
            lateralTarget,
            laneChangeSpeed * Time.fixedDeltaTime
        );

        // Moves Z by forwardSpeed units per second and X to the target lane position smoothly
        Vector3 finalMove = new Vector3(
            lateralMove.x,
            rb.position.y,
            rb.position.z + forwardMove.z
        );

        rb.MovePosition(finalMove);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );
        animator.SetBool("isGrounded", isGrounded);
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MoveLane(-1);
        if (Input.GetKeyDown(KeyCode.D))
            MoveLane(1);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
            Jump();
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isSliding)
            StartCoroutine(Slide());
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void MoveLane(int direction)
    {
        // Limits the lane to be between 0 and 2
        int targetLane = Mathf.Clamp(currentLane + direction, 0, 2);
        currentLane = targetLane;
        // x=-laneDistance if currentLane == 0, x=0 if currentLane == 1, x=laneDistance if currentLane == 2
        targetPosition.x = (currentLane - 1) * laneDistance;
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        // Reduce the height and center of the collider
        capsuleCollider.height = originalColliderHeight / 2;
        capsuleCollider.center = new Vector3(
            originalColliderCenter.x,
            originalColliderCenter.y / 2,
            originalColliderCenter.z
        );

        // Wait for the duration of the slide
        yield return new WaitForSeconds(slideDuration);

        // Restore the collider to its original size
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;

        isSliding = false;
        animator.SetBool("isSliding", false);
    }
}
