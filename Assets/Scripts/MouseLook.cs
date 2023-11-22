using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitiity = 100f;
    [SerializeField] private GameInput gameInput;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.GetCameraRotationNormalized();
        RotateCamera(inputVector);
    }

    private void RotateCamera(Vector2 input)
    {
        float mouseX = input.x * mouseSensitiity * Time.deltaTime;
        float mouseY = input.y * mouseSensitiity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -55f, 55f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -55f, 55f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
    }
}
