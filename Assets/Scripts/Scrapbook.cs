using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Scrapbook : MonoBehaviour
{
    [SerializeField] private GameObject imageHolder;
    private List<GameObject> scrapbookHolder = new List<GameObject>();

    //When the menu opens it loads all images
    private void OnEnable()
    {
        List<Photo> pictures = new List<Photo>();
        pictures = ImageDatabase.GetEveryPhoto();
        foreach (Photo img in pictures)
        {
            GameObject tempImg = Instantiate(imageHolder, gameObject.transform);
            scrapbookHolder.Add(tempImg);
            tempImg.GetComponent<RawImage>().texture = img.photo;
        }
    }

    //Destroys all images when menu closes
    //so it can load new on enable
    private void OnDisable()
    {
        foreach (GameObject image in scrapbookHolder)
        {
            Destroy(image);
        }
    }
}
