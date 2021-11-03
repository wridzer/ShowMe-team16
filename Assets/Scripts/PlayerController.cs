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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
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
    }

    public void SwitchMode()
    {
        photoMode = !photoMode;
    }

    private void moveCam(Vector3 _camPos)
    {
        mainCameraInstance.transform.localPosition = _camPos;
    }
}
