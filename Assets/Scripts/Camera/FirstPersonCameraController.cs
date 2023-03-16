using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    // Mouse sensitivity
    public float mouseSensitivity = 100f;

    // Reference to the player object
    public Transform playerTransform;

    // Rotation values
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        // Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update rotation values based on mouse input
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

        // Rotate camera and player transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
    }
}
