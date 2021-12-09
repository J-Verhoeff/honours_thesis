using UnityEngine;

// Player Controller Script
public class ThirdPersonCharacterConttroller : MonoBehaviour {
    [Header("General")]
    [SerializeField] private float inputSensitivity = 0.1f;

    [Header("Movement")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private Transform camera;

    [Header("Falling")]
    [SerializeField] private float gravityFactor = 1f;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private float closeEnough = 0.1f;

    [Header("Jumping")]
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private bool canAirControl = false;

    private CharacterController controller;
    private Animator animator;
    private float turnSmoothVelocity;
    private float verticalVelocity = 0f;
    public bool isGrounded = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("ybot").GetComponent<Animator>();
    }

    private void Update() {
        // On the ground?
        RaycastHit collision;
        if (Physics.Raycast(groundPosition.position, Vector3.down, out collision, closeEnough, platformLayer)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        // Update Vertical speed
        if (!isGrounded) {
           animator.SetBool("Falling", true);
           verticalVelocity += gravityFactor * -9.81f * Time.deltaTime;
        } else {
           verticalVelocity = 0f;
           animator.SetBool("Falling", false);
        }

        if (isGrounded && Input.GetButtonDown("Jump")) {
            verticalVelocity = jumpSpeed;
            isGrounded = false;
            // TO-DO set animator
        }

        // get inputs
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(xAxis, 0f, zAxis).normalized;

        Vector3 moveDir = Vector3.zero;
        if(dir.magnitude >= inputSensitivity) {            
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
        }
        
        Vector3 y = Vector3.up * verticalVelocity * Time.deltaTime;
        animator.SetFloat("xSpeed",  moveDir.magnitude * speed);
        controller.Move((moveDir.normalized + y.normalized) * speed * Time.deltaTime);
    }
}
