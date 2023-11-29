using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{   
    //how far player can shoot
    [SerializeField] private float range = 100f;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask enemyMask;
    private void Awake()
    {
        inputHandler.OnShoot += Shoot_OnShoot;
    }


    private void Shoot_OnShoot(object sender, EventArgs e)
    {
        Debug.Log("Shoot");
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range, enemyMask))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
