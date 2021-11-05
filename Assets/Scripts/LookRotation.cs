using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float walkModeLookSpeed = 50;
    [SerializeField] private float camModeLookSpeed = 25;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    //Check what mode the player is in
    //so it knows how fast to rotate
    void Update()
    {
        float currentRotSpeed;
        if (controller.photoMode)
        {
                currentRotSpeed = camModeLookSpeed;
        }
        else
        {
                currentRotSpeed = walkModeLookSpeed;
        }
        Rotate(currentRotSpeed);
    }

    //Calculates the rotation
    private void Rotate(float _rotateSpeed)
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        Vector3 rotation = new Vector3(0, mouseX, 0).normalized * _rotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
