﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headMovement : MonoBehaviour
{
    //Mouse Variables
    public float mouseSensitivity = 100f;


    //Body Variables
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Debug.Log(mouseX);
        //Debug.Log(mouseY);

        //Vertical momvement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Horizonal Movement
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    public void changeSensitivity(float sensitivity)
    {
        mouseSensitivity = Mathf.Lerp(50, 150, sensitivity);
    }
}
