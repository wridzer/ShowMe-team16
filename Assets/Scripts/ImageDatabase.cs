using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageDatabase
{
    private static Dictionary<int, Texture2D> takenPictures = new Dictionary<int, Texture2D>();

    public static void AddPhoto(int _number, Texture2D _photo)
    {
        takenPictures.Add(_number, _photo);
    }

    public static void RemovePhoto(int _number)
    {
        takenPictures.Remove(_number);
    }

    public static Texture2D GetSpecificPhoto(int _number)
    {
        return takenPictures[_number];
    }

    public static List<Texture2D> GetEveryPhoto()
    {
        List<Texture2D> photos = new List<Texture2D>();
        foreach(KeyValuePair<int, Texture2D> entry in takenPictures)
        {
            photos.Add(entry.Value);
        }
        return photos;
    }
}
