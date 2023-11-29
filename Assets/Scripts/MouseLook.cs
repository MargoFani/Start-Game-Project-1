using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitiity = 100f;
    [SerializeField] private InputHandler gameInput;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private bool CanRotate = false;
    private void Awake()
    {
        UIManager.OnGameStarted += StartCameraRotation_OnGameStarted;
        Game.OnEndGame += FrozeCameraRotation_OnEndGame;
    }

    private void FrozeCameraRotation_OnEndGame(object sender, Game.OnEndGameEventArgs e)
    {
        CanRotate = false;
        Debug.Log("FrozeCameraRotation_OnEndGame");
        Cursor.lockState = CursorLockMode.None;
    }

    private void StartCameraRotation_OnGameStarted(object sender, EventArgs e)
    {
        CanRotate = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanRotate)
        {
            Vector2 inputVector = gameInput.GetCameraRotationNormalized();
            RotateCamera(inputVector);
        }

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
