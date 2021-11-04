using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System.IO;
using UnityEngine.UI;

public class PhotoMode : MonoBehaviour
{

    [SerializeField] private float photoDisplayTimer = 3;
    [SerializeField] private float focusSpeed = 2;
    [SerializeField] private GameObject cameraInstance;
    [SerializeField] private GameObject postprocessing;
    [SerializeField] private GameObject imageDisplay;
    [SerializeField] private float focus = 300f;
    private int fileCounter = 0;
    private PostProcessVolume volume;
    private DepthOfField depthOfField;
    private Camera cam;

    void Start()
    {
        volume = postprocessing.GetComponent<PostProcessVolume>();
        cam = cameraInstance.GetComponent<Camera>();
    }

    
    void Update()
    {
        Photomode();
    }

    //Handles focus and makes picture
    private void Photomode()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TakePhoto();
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel") * 1000 * focusSpeed;
        volume.profile.TryGetSettings(out depthOfField);
        focus -= scroll * Time.deltaTime;
        depthOfField.focalLength.value = focus;
    }

    //takes photo
    private void TakePhoto()
    {
        //Creates rendertexture
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        //Let camera render
        cam.Render();

        //Add render to rendertexture
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        //Display and save image
        DisplayImage(image);
        ImageDatabase.AddPhoto(fileCounter, image);

        //switch playermode and add to filecounter
        GetComponent<PlayerController>().SwitchMode();
        fileCounter++;
    }

    //Display image
    private void DisplayImage(Texture2D _photo)
    {
        imageDisplay.GetComponent<RawImage>().texture = _photo;
        imageDisplay.SetActive(true);
        StartCoroutine(photoTimer());
    }

    //Times photodisplay after capture
    private IEnumerator photoTimer()
    {
        yield return new WaitForSeconds(photoDisplayTimer);
        imageDisplay.SetActive(false);
    }

    //Resets focus
    private void OnDisable()
    {
        volume.profile.TryGetSettings(out depthOfField);
        depthOfField.focalLength.value = 1;
    }
}
