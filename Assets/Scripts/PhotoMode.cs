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
    [SerializeField] private float focus = 5;
    [SerializeField] private float maxFocus = 75;
    [SerializeField] private float minFocus = 5;
    [SerializeField] private float apatureMin = 10;
    [SerializeField] private float apatureMax = 32;
    [SerializeField] private float addShake;
    [SerializeField] private AudioClip endOffFocus;
    [SerializeField] private AudioClip captureSound;
    private int fileCounter = 0;
    private PostProcessVolume volume;
    private DepthOfField depthOfField;
    private Camera cam;
    private AudioSource audioS;

    void Start()
    {
        volume = postprocessing.GetComponent<PostProcessVolume>();
        cam = cameraInstance.GetComponent<Camera>();
        audioS = GetComponent<AudioSource>();
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
        FocusControl();
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
        Photo photo = new Photo();
        photo.photo = image;
        photo.timeWhenTaken = Time.realtimeSinceStartup;
        ImageDatabase.AddPhoto(fileCounter, photo);

        //switch playermode and add to filecounter and playsound
        GetComponent<PlayerController>().SwitchMode();
        fileCounter++;
        audioS.PlayOneShot(captureSound);
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
        depthOfField.focusDistance.value = 1;
    }

    //Focussing/Controlling postprocessing
    private void FocusControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * 100 * focusSpeed;
        volume.profile.TryGetSettings(out depthOfField);
        focus += scroll * Time.deltaTime;
        focus = Mathf.Clamp(focus, minFocus, maxFocus);
        depthOfField.focusDistance.value = focus;
        if(focus == minFocus)
        {
            focus++;
            GetComponent<PlayerController>().shake += addShake;
            audioS.PlayOneShot(endOffFocus);
        }
        if (focus == maxFocus)
        {
            focus--;
            GetComponent<PlayerController>().shake += addShake;
            audioS.PlayOneShot(endOffFocus);
        }
        //Time.timeScale = 1 - (focus / 100);
        depthOfField.aperture.value = apatureMax + (focus - 5) / (minFocus - maxFocus) * (apatureMax - apatureMin);
        Debug.Log(focus);
        Debug.Log(depthOfField.aperture.value);
    }
}
