using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    [SerializeField] float jumpForce = 10f;

    [SerializeField] float sensitivity = 10f;

    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;

    [SerializeField] Transform playerCamera;

    Rigidbody playerRb;

    Vector2 currentMouseDelta;

    Vector2 currentMouseDeltaVelocity;

    float cameraCap;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        MovePlayerCamera();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerMovementInput = transform.TransformDirection(playerMovementInput) * speed;
        playerRb.velocity = new Vector3(playerMovementInput.x, playerRb.velocity.y, playerMovementInput.z);

        
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void MovePlayerCamera()
    {
        /*
        Vector2 playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float xRot =- playerMouseInput.y * sensitivity;

        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);

        playerCamera.transform.position = transform.position;

        playerCamera.transform.rotation = transform.rotation;*/

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * sensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }
}
