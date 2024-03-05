using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;


public class PredictionOutputBox : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material mat;
    public string red;
    public string green;
    public string blue;
    float timer;
    [SerializeField] private Color32 newColor;
    [SerializeField] private bool ChangeColorByPrediction = false;

    [SerializeField] List<double> MSEList = new List<double>();
    // Start is called before the first frame update
    void Start()
    {
        
        renderer = this.renderer;
        mat = renderer.material;
        red = mat.color.r.ToString();
        green = mat.color.g.ToString();
        blue = mat.color.b.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log("rgb " + red + " " + green + " " + blue);
        newColor = new Color(float.Parse(red, CultureInfo.InvariantCulture.NumberFormat), float.Parse(green, CultureInfo.InvariantCulture.NumberFormat), float.Parse(blue, CultureInfo.InvariantCulture.NumberFormat));

        renderer.material.color = newColor;
        Debug.Log("render mat " + newColor);
        */

        if (!ChangeColorByPrediction)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0f)//change the float value here to change how long it takes to switch.
            {
                Debug.Log("t pressed");
                // pick a random color
                Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                Debug.Log("render mat " + newColor);
                // apply it on current object's material
                renderer.material.color = newColor;
                timer = 0;
            }


        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)//change the float value here to change how long it takes to switch.
            {
                Debug.Log("rgb " + red + " " + green + " " + blue);
                Debug.Log("red float " + float.Parse(red));
                newColor = new Color(float.Parse(red)/255, float.Parse(green)/255, float.Parse(blue)/255, 1);
                Debug.Log("render mat " + newColor);
                renderer.material.color = newColor;
                

            }
             
        }
    }

    [SerializeField] ColorThief colorTheif;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f)//change the float value here to change how long it takes to switch.
        {
            MSEList.Add(CalculateMSE(colorTheif.colorPalette[0].r, colorTheif.colorPalette[0].g, colorTheif.colorPalette[0].b, float.Parse(red) / 255, float.Parse(green) / 255, float.Parse(blue) / 255));
            //Debug.Log("mse " + mse1);
        }
    }

    private double CalculateMSE(float inputR, float inputG, float inputB, float predictedR, float predictedG, float predictedB)
    {
        double mse = (inputR - predictedR)* (inputR - predictedR) +
                     (inputG - predictedG)* (inputG - predictedG) +
                     (inputB - predictedB)* (inputB - predictedB);

        mse /= 3; // Divide by 3 to get the average squared error
        return mse;
    }

}
