using System;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise : MonoBehaviour
{
    public int width;
    public int height;
    public float scale = 20f;
    public float offsetY = 0f;
    public float offsetX = 0f;
    
    public Image image;

    private void Start()
    {
        TextureUpdate();
    }

    /* --- Only used for debuging
    private void Update()
    {
        TextureUpdate();
    }
    */

    public void TextureUpdate()
    {
        image.material.mainTexture = GenerateTexture();
        image.enabled = false;
        image.enabled = true;
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color color = CalculateColor(i, j);
                texture.SetPixel(i,j,color);
            }
        }
        
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
