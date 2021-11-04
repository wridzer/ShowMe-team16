using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float maxSpeed = 20;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    //Handle movement input
    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputY, 0, -inputX).normalized * moveSpeed * Time.deltaTime;
        rb.AddRelativeForce(movement, ForceMode.Impulse);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), 0, Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
    }
}
