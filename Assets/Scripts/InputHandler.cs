using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private float timer;
    [SerializeField] private float coolDown = 0.5f;
    public event EventHandler OnShoot;
    private PlayerInputActions playerInputActions;
    private bool CanShoot;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        timer = 0;
        CanShoot = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
            //Debug.Log("timer > 0 ");
            timer -= Time.deltaTime;
            //Debug.Log("timer = " + timer);
            CanShoot = false;
            //Debug.Log("CanShoot = " + CanShoot);

        }
        else { 
            CanShoot = true;
            //Debug.Log("CanShoot = " + CanShoot);
            timer = 0;
        }
    }

    public Vector2 GetCameraRotationNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.RotateCamera.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;

    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (CanShoot)
        {
            timer = coolDown;
            //Debug.Log("timer = cooldown : " + timer);
            OnShoot?.Invoke(this, EventArgs.Empty);
        }

    }

}
