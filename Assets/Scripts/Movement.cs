using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeedNormal = 10;
    [SerializeField] private float moveSpeedCamera = 3;
    [SerializeField] private float maxSpeed = 20;
    private Rigidbody rb;
    private PlayerController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    //Determines walkspeed
    void Update()
    {
        float currentWalkSpeed;
        if (controller.photoMode)
        {
                currentWalkSpeed = moveSpeedCamera;
        }
        else
        {
            currentWalkSpeed = moveSpeedNormal;

        }
        Move(currentWalkSpeed);
    }

    //Handle movement input
    private void Move(float _moveSpeed)
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputY, 0, -inputX).normalized * _moveSpeed * Time.deltaTime;
        rb.AddRelativeForce(movement, ForceMode.Impulse);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), 0, Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed)); //gives player a max speed(velocity)
    }
}
