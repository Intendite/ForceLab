using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Billboard : MonoBehaviour
{
    private Transform cam;
    private TMP_Text massText;
    private Rigidbody boxRigidbody;
    private Vector3 initialOffset; // Store the initial offset between text and box

    public float distanceAboveBox = 0.5f; // Adjust this to set the desired distance above the box

    // Start is called before the first frame update
    void Start()
    {
        // Automatically find and link the main camera
        cam = Camera.main.transform;

        // Find the Text component in children (assuming there's only one Text component)
        massText = GetComponentInChildren<TMP_Text>();

        // Get the parent GameObject (which is "box") of the canvas
        Transform parent = transform.parent;
        if (parent != null)
        {
            // Try to get the Rigidbody component from the "box" GameObject
            boxRigidbody = parent.GetComponent<Rigidbody>();

            if (boxRigidbody == null)
            {
                Debug.LogError("No Rigidbody found on the parent 'box' GameObject.");
            }
        }
        else
        {
            Debug.LogError("Canvas has no parent. Make sure it's a child of 'box'.");
        }

        // Calculate and store the initial offset between text and box
        if (boxRigidbody != null)
        {
            initialOffset = transform.position - boxRigidbody.transform.position;
        }
    }

    void LateUpdate()
    {
        // Make sure the massText reference is not null and boxRigidbody is not null
        if (massText != null && boxRigidbody != null)
        {
            // Display the mass of the box's Rigidbody as a whole number with "Kg"
            massText.text = Mathf.Round(boxRigidbody.mass) + " Kg";

            // Calculate the desired position above the box based on the initial offset and distanceAboveBox
            Vector3 desiredPosition = boxRigidbody.transform.position + initialOffset.normalized * distanceAboveBox;

            // Set the position of the text
            massText.transform.position = desiredPosition;

            // Always face the camera
            massText.transform.LookAt(massText.transform.position + cam.forward);
        }
    }
}
