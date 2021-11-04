using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Scrapbook : MonoBehaviour
{
    private string myPath = "/Pics/";
    [SerializeField] private GameObject imageHolder;
    private List<GameObject> scrapbookHolder = new List<GameObject>();

    private void OnEnable()
    {
        List<Texture2D> pictures = new List<Texture2D>();
        pictures = ImageDatabase.GetEveryPhoto();
        foreach (Texture2D img in pictures)
        {
            GameObject tempImg = Instantiate(imageHolder, gameObject.transform);
            scrapbookHolder.Add(tempImg);
            tempImg.GetComponent<RawImage>().texture = img;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject image in scrapbookHolder)
        {
            Destroy(image);
        }
    }
}
