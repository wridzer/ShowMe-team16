using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float walkModeLookSpeed = 50;
    [SerializeField] private float camModeLookSpeed = 25;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotSpeed;
        switch (GetComponent<PlayerController>().photoMode)
        {
            case true:
                currentRotSpeed = camModeLookSpeed;
                break;
            case false:
                currentRotSpeed = walkModeLookSpeed;
                break;
        }
        Rotate(currentRotSpeed);
    }

    private void Rotate(float _rotateSpeed)
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        Vector3 rotation = new Vector3(0, mouseX, 0).normalized * _rotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
