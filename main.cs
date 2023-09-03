using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 2.0f;
    public float rotationSpeed = 2.0f;

    private Camera mainCamera;
    private float initialRotationX;
    private float initialRotationY;
    private bool isRotating = false;

    private void Start()
    {
        mainCamera = Camera.main;
        initialRotationX = mainCamera.transform.eulerAngles.x;
        initialRotationY = mainCamera.transform.eulerAngles.y;
    }

    private void Update()
    {
        // Zoom in and out using the trackpad (scroll wheel)
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.transform.Translate(Vector3.forward * zoomInput * zoomSpeed * Time.deltaTime);

        // Rotate the camera while holding right mouse button and using trackpad
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            initialRotationX -= mouseY;
            initialRotationY += mouseX;

            // Limit the vertical rotation to avoid camera flipping
            initialRotationX = Mathf.Clamp(initialRotationX, -90f, 90f);

            // Apply rotation
            Quaternion rotation = Quaternion.Euler(initialRotationX, initialRotationY, 0);
            mainCamera.transform.rotation = rotation;
        }
    }
}

