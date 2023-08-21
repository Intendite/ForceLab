using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private float mouseSens = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Translation Movement
        if (Input.GetKey(KeyCode.W))
            this.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.A))
            this.transform.Translate(Vector3.back * playerSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.S))
            this.transform.Translate(Vector3.left * playerSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.D))
            this.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime, Space.Self);

        float mouseX = Input.GetAxis("Horizontal");
        float mouseY = Input.GetAxis("Vertical");

        this.transform.Rotate(0, mouseX * mouseSens * Time.deltaTime, mouseY * mouseSens * Time.deltaTime);
    }
}
