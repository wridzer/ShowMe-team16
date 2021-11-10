using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool photoMode;
    [SerializeField] private GameObject mainCameraInstance;
    [SerializeField] private Vector3 thirdPersonView = new Vector3(-3, 2, 0);
    [SerializeField] private Vector3 firstPersonView = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject cameraOverlay;
    [SerializeField] private GameObject walkOverlay;
    [SerializeField] private GameObject scrapBook;

    public float shake  = 0;
    [SerializeField] private float shakeAmount  = 0.7f;
    [SerializeField] private float decreaseFactor  = 1.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Handles the photomode and scrapbook
    void Update()
    {
        if (photoMode)
        {
            GetComponent<PhotoMode>().enabled = true;
            moveCam(firstPersonView);
            cameraOverlay.SetActive(true);
            walkOverlay.SetActive(false);
            scrapBook.SetActive(false);
        }
        else {
            GetComponent<PhotoMode>().enabled = false;
            moveCam(thirdPersonView);
            cameraOverlay.SetActive(false);
            walkOverlay.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchMode();
        }

        if (Input.GetKeyDown(KeyCode.E) && !photoMode)
        {
            scrapBook.SetActive(!scrapBook.activeSelf);
        }

        ScreenShake();
    }

    //Changes photomode
    public void SwitchMode()
    {
        photoMode = !photoMode;
    }

    //Move camera on mode change
    private void moveCam(Vector3 _camPos)
    {
        mainCameraInstance.transform.localPosition = _camPos;
    }

    private void ScreenShake()
    {
        if (shake > 0)
        {
            mainCameraInstance.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0;
        }
    }
}
