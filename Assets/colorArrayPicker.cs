using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorArrayPicker : MonoBehaviour
{
    // Assuming you have a reference to the Render Texture
    RenderTexture renderTexture;

    // Assuming the Render Texture has a resolution of 60x60 pixels
    int pixelWidth = 60;
    int pixelHeight = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Create a 2D array to store the Vector3 RGB values
            Vector3[,] rgbArray = new Vector3[pixelWidth, pixelHeight];

            // Create a temporary Texture2D to read the pixels from the Render Texture
            Texture2D tempTexture = new Texture2D(pixelWidth, pixelHeight, TextureFormat.RGBA32, false);

            // Set the active Render Texture to the one you want to read from
            RenderTexture.active = renderTexture;

            // Read the pixels from the Render Texture into the temporary Texture2D
            tempTexture.ReadPixels(new Rect(0, 0, pixelWidth, pixelHeight), 0, 0);
            tempTexture.Apply();

            // Retrieve the colors from the temporary Texture2D and populate the rgbArray
            Color[] pixels = tempTexture.GetPixels();
            for (int y = 0; y < pixelHeight; y++)
            {
                for (int x = 0; x < pixelWidth; x++)
                {
                    int index = y * pixelWidth + x;
                    Color pixelColor = pixels[index];
                    Vector3 rgb = new Vector3(pixelColor.r, pixelColor.g, pixelColor.b);
                    rgbArray[x, y] = rgb;
                }
            }

            // Clean up resources
            RenderTexture.active = null;
            Destroy(tempTexture);
        }
    }
}
