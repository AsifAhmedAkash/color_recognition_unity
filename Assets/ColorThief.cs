using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ColorThief : MonoBehaviour
{
     // Assign your image texture in the Inspector
    [SerializeField] private RenderTexture rTex;
    public Texture2D imageTexture;

    public Color[] colorPalette;

    private Color[] GetColorPalette(Texture2D texture, int colorCount)
    {
        // Resize the texture to a smaller size for sampling
        int sampleSize = 5;
        Texture2D sampledTexture = new Texture2D(sampleSize, sampleSize);
        Graphics.CopyTexture(texture, sampledTexture);

        // Create an array to store pixel colors
        Color[] pixelColors = sampledTexture.GetPixels();

        // Calculate color frequencies
        var colorFrequencies = pixelColors
            .GroupBy(color => color)
            .OrderByDescending(group => group.Count())
            .Take(colorCount)
            .Select(group => group.Key)
            .ToArray();

        return colorFrequencies;
    }

    private void updates()
    {
        int colorCount = 2; // Number of colors to extract
        colorPalette = GetColorPalette(imageTexture, colorCount);
        /*
        Debug.Log("Extracted Color Palette:" + colorPalette.Length);
        foreach (Color color in colorPalette)
        {
            Debug.Log($"R: {color.r}, G: {color.g}, B: {color.b}");
        }
        */
    }

    private void FixedUpdate()
    {
        imageTexture = toTexture2D(rTex);
        updates();

    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(5, 5);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
