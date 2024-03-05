using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color color;
    public RenderTexture renderTexture;
    [SerializeField] private Text RGBtext;
    [SerializeField] private Text HSVtext;

    public Vector3 HSVValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;

        
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

        RenderTexture.active = null;
        color = texture.GetPixel(100, 100);

        // Output the color to the console
        //Debug.Log("RGB value " + color);
        //Debug.Log("HSV value " + RGBToHSV(color));
        RGBtext.text = color.ToString();
        
        HSVValue = RGBToHSV(color);
        HSVtext.text = "HSV " + HSVValue.ToString();
    }

    public Vector3 RGBToHSV(Color rgbColor)
    {
        float r = rgbColor.r;
        float g = rgbColor.g;
        float b = rgbColor.b;

        float max = Mathf.Max(r, Mathf.Max(g, b));
        float min = Mathf.Min(r, Mathf.Min(g, b));
        float delta = max - min;

        float hue = 0f;
        if (delta != 0f)
        {
            if (max == r)
            {
                hue = (g - b) / delta;
            }
            else if (max == g)
            {
                hue = 2f + (b - r) / delta;
            }
            else
            {
                hue = 4f + (r - g) / delta;
            }

            hue *= 60f;
            if (hue < 0f)
            {
                hue += 360f;
            }
        }

        float saturation = (max == 0f) ? 0f : delta / max;

        float value = max;

        return new Vector3(hue, saturation, value);
    }
}
