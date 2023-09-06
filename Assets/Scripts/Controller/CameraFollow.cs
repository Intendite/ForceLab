using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object to follow (your box)
    public Rigidbody boxRigidbody; // The object's RigidBody
    public Transform playerTransform; // Reference to the player's transform

    private bool isFollowing = false;
    private Vector3 originalPosition;
    private Vector3 originalBoxPosition;

    private Camera mainCamera;

    private void Start()
    {
        originalPosition = transform.position;
        originalBoxPosition = target.position;
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (target == null || playerTransform == null)
            return;

        // Check if the target's Y position is below the player's Y position minus 10
        if (target.position.y < playerTransform.position.y - 10f && target.position.y > 3)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = targetPosition;
        }

        else if (target.position.y <= 3 && boxRigidbody.velocity.magnitude == 0)
        {
            Debug.Log("Reset Positions");
            // Box has touched the ground, teleport both camera and box to their original positions
            transform.position = originalPosition;
            target.position = originalBoxPosition;
        }

        // Adjust the camera's FOV
        mainCamera.fieldOfView = 90f;
    }
}
