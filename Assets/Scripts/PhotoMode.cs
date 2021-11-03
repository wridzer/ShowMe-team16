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

    // Start is called before the first frame update
    void Start()
    {
        volume = postprocessing.GetComponent<PostProcessVolume>();
        cam = cameraInstance.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Photomode();
    }

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
        DisplayImage(image);
        GetComponent<PlayerController>().SwitchMode();
        //Destroy(image);

        File.WriteAllBytes(Application.dataPath + "/Pics/" + fileCounter + ".png", bytes);
        fileCounter++;
    }

    private void DisplayImage(Texture2D _photo)
    {
        imageDisplay.GetComponent<RawImage>().texture = _photo;
        imageDisplay.SetActive(true);
        StartCoroutine(photoTimer());
    }

    private IEnumerator photoTimer()
    {
        yield return new WaitForSeconds(photoDisplayTimer);
        imageDisplay.SetActive(false);
    }
}
