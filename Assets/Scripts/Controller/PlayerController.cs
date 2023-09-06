using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float mouseSensitivity = 5.0f;

    private Camera playerCamera;

    private void Start()
    {
        // Get the camera component attached to the player
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Translation Movement
        MovePlayer();

        // Mouse Rotation
        RotatePlayerWithMouse();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = Vector3.zero;

        // Check for movement input
        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector3.right;

        // Normalize the direction vector to ensure consistent speed in all directions
        moveDirection.Normalize();

        // Translate the player based on the input
        transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.Self);
    }

    private void RotatePlayerWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player based on mouse input (horizontal and vertical)
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.left * mouseY * mouseSensitivity * Time.deltaTime);
    }
}
