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
        Object[] images = Resources.LoadAll(Application.dataPath + myPath, typeof(Image));
        foreach (Image img in images)
        {
            GameObject tempImg = Instantiate(imageHolder, gameObject.transform);
            scrapbookHolder.Add(tempImg);
            tempImg.GetComponent<Image>().sprite = img.sprite;
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
