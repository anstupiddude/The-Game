using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    CharacterController characterController;

    [Header("Player Options")]
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    
    [Header("Cam Options")]
    [SerializeField] private Camera cam;
    public float mouseSensitivity;
    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    float h_mouse, v_mouse;

    private Vector3 move = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        h_mouse = mouseSensitivity * Input.GetAxis("Mouse X");
        v_mouse += mouseSensitivity * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, -65.0f, 60.0f);
        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);
        transform.Rotate(0, h_mouse, 0);

        if (characterController.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift))
                move = transform.TransformDirection(move) * runSpeed;
            else
                move = transform.TransformDirection(move) * walkSpeed;

            if (Input.GetMouseButton(1))
            {
                runSpeed = 2.0f;
                walkSpeed = 2.0f;
            }

            else
            {
                runSpeed = 10.0f;
                walkSpeed = 8.0f;
            }

            if (Input.GetKey(KeyCode.Space))
                move.y = jumpSpeed;

        }
        move.y -= gravity * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);
    }
}
