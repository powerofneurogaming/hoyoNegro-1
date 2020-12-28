using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code inspired + copied from Brackeys First Person Movement

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;   
    public float movementSpeed = 12f;
    public AudioSource clickSound;

    public bool MoveAlongCamera = true;
    Vector3 move;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Find variables for the 3-axis
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Float");
        float z = Input.GetAxis("Vertical");

        if (!MoveAlongCamera)
        {
            move = transform.right * x + transform.up * y + transform.forward * z;
        }
        else
        {
            move = Camera.main.transform.forward * z + transform.up * y + Camera.main.transform.right * x;
        }


        controller.Move(move*movementSpeed*Time.deltaTime);
    }

    public void makeSound()
    {
        clickSound.Play();
    }
}
