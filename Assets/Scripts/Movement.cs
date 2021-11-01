using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float rotateWalkSpeed = 50;
    [SerializeField] private float camLookSpeed = 25;
    [SerializeField] private GameObject cameraInstance;
    [SerializeField] private Vector3 thirdPersonView = new Vector3(-3, 2, 0);
    [SerializeField] private Vector3 firstPersonView = new Vector3(0, 0.5f, 0);
    private bool photoMode = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (photoMode)
        {
            Rotate(camLookSpeed);
        }
        if (!photoMode)
        {
            Move();
            Rotate(rotateWalkSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchPhotoMode();
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputY, 0, -inputX).normalized * moveSpeed * Time.deltaTime;
        rb.AddRelativeForce(movement, ForceMode.Impulse);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), 0, Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
    }

    private void Rotate(float _rotateSpeed)
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        Vector3 rotation = new Vector3(0, mouseX, 0).normalized * _rotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void SwitchPhotoMode()
    {
        if (photoMode)
        {
            cameraInstance.transform.localPosition = thirdPersonView;
        }
        if (!photoMode)
        {
            cameraInstance.transform.localPosition = firstPersonView;
        }
        photoMode = !photoMode;
    }

    private void Photomode()
    {

    }

    private void TakePhoto()
    {

    }
}
