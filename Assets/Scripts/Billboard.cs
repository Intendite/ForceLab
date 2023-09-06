using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;
    private TMP_Text massText;
    private Rigidbody boxRigidbody;
    private Vector3 initialOffset;

    void Start()
    {
        // Get the main camera's transform
        cam = Camera.main.transform;

        // Find the TMP_Text component within children
        massText = GetComponentInChildren<TMP_Text>();

        // Ensure that this Billboard component is a child of 'box'
        Transform parent = transform.parent;

        if (parent != null)
        {
            boxRigidbody = parent.GetComponent<Rigidbody>();

            if (boxRigidbody == null)
            {
                Debug.LogError("No Rigidbody found on the parent 'box' GameObject.");
            }
            else
            {
                // Calculate the initial offset between the Billboard and the 'box'
                initialOffset = transform.position - boxRigidbody.transform.position;
            }
        }
        else
        {
            Debug.LogError("Canvas has no parent. Make sure it's a child of 'box'.");
        }
    }

    void LateUpdate()
    {
        if (massText != null && boxRigidbody != null)
        {
            // Update the mass text
            massText.text = Mathf.Round(boxRigidbody.mass) + " Kg";

            // Calculate the desired position above the 'box'
            Vector3 desiredPosition = boxRigidbody.transform.position + initialOffset.normalized;

            // Set the position of the mass text
            massText.transform.position = desiredPosition;

            // Make the mass text face the camera
            massText.transform.LookAt(massText.transform.position + cam.forward);
        }
    }
}
