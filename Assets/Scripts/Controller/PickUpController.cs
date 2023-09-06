using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("PickUp Settings")]
    [SerializeField] private Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
        }
    }

    private void TryPickupObject()
    {
        RaycastHit hit;

        // Cast a ray from the camera forward to detect objects within pickupRange
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            GameObject hitObject = hit.transform.gameObject;

            // Check if the hit object has a Rigidbody and is tagged as "Interactive"
            if (hitObject.GetComponent<Rigidbody>() && hitObject.CompareTag("Interactive"))
            {
                PickupObject(hitObject);
            }
        }
    }

    private void MoveObject()
    {
        // Calculate the move direction
        Vector3 moveDirection = holdArea.position - heldObj.transform.position;

        // Apply force to move the object towards the holdArea
        heldObjRB.AddForce(moveDirection * pickupForce);
    }

    private void PickupObject(GameObject pickedObject)
    {
        // Get the Rigidbody component and set physics properties
        heldObjRB = pickedObject.GetComponent<Rigidbody>();
        heldObjRB.useGravity = false;
        heldObjRB.drag = 10;
        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        // **Set the velocity of the rigidbody to zero**
        heldObjRB.velocity = Vector3.zero;

        // Set the picked object as a child of the holdArea
        pickedObject.transform.parent = holdArea;

        // Store the picked object
        heldObj = pickedObject;

        // Play a pickup sound
        FindObjectOfType<AudioManager>().Play("PickUp/Drop");
    }


    private void DropObject()
    {
        // Restore the physics properties of the held object
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        // Remove the object from the holdArea parent
        heldObj.transform.parent = null;

        // Clear the held object reference
        heldObj = null;

        // Play a drop sound
        FindObjectOfType<AudioManager>().Play("PickUp/Drop");
    }
}
