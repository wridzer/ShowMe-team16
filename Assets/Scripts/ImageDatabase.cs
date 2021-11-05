using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageDatabase
{
    //Dictionary that holds the images
    private static Dictionary<int, Photo> takenPictures = new Dictionary<int, Photo>();

    //for adding pictures to database
    public static void AddPhoto(int _number, Photo _photo)
    {
        takenPictures.Add(_number, _photo);
    }

    //for removing pictures to database
    public static void RemovePhoto(int _number)
    {
        takenPictures.Remove(_number);
    }

    //for getting a specific picture from database
    public static Photo GetSpecificPhoto(int _number)
    {
        return takenPictures[_number];
    }

    //getting all photos
    public static List<Photo> GetEveryPhoto()
    {
        List<Photo> photos = new List<Photo>();
        foreach(KeyValuePair<int, Photo> entry in takenPictures)
        {
            photos.Add(entry.Value);
        }
        return photos;
    }
}
