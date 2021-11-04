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
    [SerializeField] private GameObject scrapBook;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Handles the photomode and scrapbook
    void Update()
    {
        switch (photoMode)
        {
            case true:
                GetComponent<Movement>().enabled = false;
                GetComponent<PhotoMode>().enabled = true;
                moveCam(firstPersonView);
                cameraOverlay.SetActive(true);
                break;
            case false:
                GetComponent<Movement>().enabled = true;
                GetComponent<PhotoMode>().enabled = false;
                moveCam(thirdPersonView);
                cameraOverlay.SetActive(false);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchMode();
        }

        if (Input.GetKeyDown(KeyCode.E) && !photoMode)
        {
            scrapBook.SetActive(!scrapBook.activeSelf);
        }
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
}
