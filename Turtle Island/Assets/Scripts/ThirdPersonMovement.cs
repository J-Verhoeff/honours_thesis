using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {

    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float gravity = -9.81f;
    public Transform groundPosition;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    private Vector3 velocity;
    private bool isGrounded;

    private void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(groundPosition.position, groundDistance, groundLayer);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animator.SetFloat("Speed", speed);
        } else {
            animator.SetFloat("Speed", 0f);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity* Time.deltaTime);
    }
}
