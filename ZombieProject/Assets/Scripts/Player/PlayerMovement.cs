using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed;
    public float playerRunSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    [Header("Ground Checkers")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool showGroundCheckGizmo;
    //public bool isMoving;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Camera cam;
    public float xRotation;

    private Vector3 _velocity;
    private bool _isGrounded;
    public bool isRunning;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGround();
        MouseLook();
        PlayerMove();
        JumpGravity();
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89.0f, 89.0f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && z > 0)
        {
            controller.Move(move * playerRunSpeed * Time.deltaTime);
            isRunning = true;
        }
        else
        {
            isRunning = false;
            controller.Move(move * playerSpeed * Time.deltaTime);
        }
    }

    private void JumpGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void CheckIsGround()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGroundCheckGizmo)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
        else
        {
            return;
        }
        
    }
}
