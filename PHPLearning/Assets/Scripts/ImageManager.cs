using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageManager : MonoBehaviour
{
    public static ImageManager instance;

    string _basePath;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        instance = this;

        // Make a base path
        _basePath = Application.persistentDataPath + "/Images/";
        if(!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    // check if image already exists
    bool ImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    //save images
    public void SaveImage(string name, byte[] bytes)
    {
        File.WriteAllBytes(_basePath + name, bytes);
    }
    //load images
    public byte[] LoadImage(string name)
    {
        byte[] bytes = new byte[0];
        if (ImageExists(name))
        {
            bytes = File.ReadAllBytes(_basePath + name);
        }
        return bytes;
    }

    //convert bytes into sprite
    public Sprite BytesToSprite(byte[] bytes)
    {
        //create texture2D
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        //create sprite (to be places in UI)
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
}
