using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PixelsHandler : MonoBehaviour
{
    public Slider radius;
    public Slider strength;
    public Toggle colorSwitch;
    public Toggle constantPrediction;

    private List<PixelHandler> pixels = new List<PixelHandler>();
    
    public GameObject pixelPrefab;
    public Transform pixelContainer;
    
    public PredictionClient client;

    public TextMeshProUGUI predictionText;
    private string prediction;
    public Color newColor;
    [SerializeField] private PredictionOutputBox predictionOutputBox;
    
    private void Start()
    {
        
        for (var i = 0; i < 28*28; i++)
        {
            var newPixel = Instantiate(pixelPrefab, transform.position, Quaternion.identity);
            newPixel.transform.SetParent(pixelContainer, true);
            pixels.Add(newPixel.GetComponent<PixelHandler>());
        }
    }

    private void Update()
    {
        predictionText.text = prediction;

        if (!Input.GetMouseButton(0)) return;

        var mousePosition = Input.mousePosition;
        var pixelsToChange = Physics2D.OverlapCircleAll(mousePosition, radius.value * 5)
            .Where(p => p.GetComponent<PixelHandler>() != null).Select(p => p.GetComponent<PixelHandler>()).ToArray();
        
        if (constantPrediction.isOn && pixelsToChange.Length != 0) Predict();
        
        foreach (var pixel in pixelsToChange)
        {
            var pixelColor = pixel.GetColor();
            
            if (colorSwitch.isOn) pixel.ChangePixelColor(pixelColor - strength.value);
            else pixel.ChangePixelColor(pixelColor + strength.value);
        }
    }

    public void Reset()
    {
        foreach (var pixel in pixels) pixel.ChangePixelColor(colorSwitch.isOn ? 1 : 0);
        prediction = "";
    }

    public void PredictUI()
    {
        if (constantPrediction.isOn) return;
        Predict();
    }
    
    private void Predict()
    {
        var input = ReadPixels();
        
        client.Predict(input, output =>
        {
            // Convert the output to a comma-separated string
            //string outputString = string.Join(", ", output);

            // Log the output to the Unity console
            //Debug.Log("Output: " + outputString);

            //Debug.Log("Output: " + string.Join(", ", output));
            //
            var outputMax = output.Max();
            var maxIndex = Array.IndexOf(output, outputMax);
            ///
            int outint = (int)output[0];
            //string inn = string.Join(", ", output);
            string inn = outint.ToString();
            int length = inn.Length;
            int lastPartLength = 3;

            string lastPart = inn.Substring(length - lastPartLength);
            string middlePart = inn.Substring(length - 2 * lastPartLength, lastPartLength);
            string firstPart = inn.Substring(0, length - 2 * lastPartLength);
            ///
            prediction = "Prediction: " + firstPart + ","+ middlePart + ","+ lastPart;
            predictionOutputBox.red = firstPart;
            predictionOutputBox.green = middlePart;
            predictionOutputBox.blue = lastPart;
            //newColor = new Color(float.Parse(firstPart, CultureInfo.InvariantCulture.NumberFormat), float.Parse(middlePart, CultureInfo.InvariantCulture.NumberFormat), float.Parse(lastPart, CultureInfo.InvariantCulture.NumberFormat));

            //renderer.material.color = newColor;
            //Debug.Log("render mat " + mat.ToString());
        }, error =>
        {
            // TODO: when i am not lazy
        });
    }

    [SerializeField] ColorThief colorTheif;

    public float[] input_color;
    public float[] ReadPixels()
    {
        input_color[0] = colorTheif.colorPalette[0].r * 255;
        input_color[1] = colorTheif.colorPalette[0].g * 255;
        input_color[2] = colorTheif.colorPalette[0].b * 255;
        input_color[3] = colorTheif.colorPalette[1].r * 255;
        input_color[4] = colorTheif.colorPalette[1].g * 255;
        input_color[5] = colorTheif.colorPalette[1].b * 255;


        Debug.Log("input_color " + input_color[1]);
        return input_color;
    }

}