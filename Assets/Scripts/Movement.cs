using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float rotateWalkSpeed = 50;
    [SerializeField] private float camLookSpeed = 25;
    [SerializeField] private GameObject mainCameraInstance;
    [SerializeField] private GameObject cameraInstance;
    [SerializeField] private Vector3 thirdPersonView = new Vector3(-3, 2, 0);
    [SerializeField] private Vector3 firstPersonView = new Vector3(0, 0.5f, 0);
    private Camera cam;
    private bool photoMode = false;
    public int fileCounter = 0;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = cameraInstance.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (photoMode)
        {
            Rotate(camLookSpeed);
            Photomode();
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
            mainCameraInstance.transform.localPosition = thirdPersonView;
        }
        if (!photoMode)
        {
            mainCameraInstance.transform.localPosition = firstPersonView;
        }
        photoMode = !photoMode;
    }

    private void Photomode()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TakePhoto();
        }
    }

    private void TakePhoto()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        cam.Render();

        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        File.WriteAllBytes(Application.dataPath + "/Pics/" + fileCounter + ".png", bytes);
        fileCounter++;
    }
}
