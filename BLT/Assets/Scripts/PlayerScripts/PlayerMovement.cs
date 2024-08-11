using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private bool jumpRequest;
    public bool isGrounded;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Jump Mechanics
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }

        // Sprint Mechanics
        if (isGrounded && Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Toggle Sprint
            isSprinting = !isSprinting;
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            isSprinting = false;
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput);
        moveDir = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDir;
        moveDir.Normalize();

        Vector3 velocity = moveDir * currentSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (jumpRequest)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequest = false;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.7f)
            {
                isGrounded = true;
            }
        }
    }
}
