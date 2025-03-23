using UnityEngine;
using Unity.Barracuda;
using UnityEngine.InputSystem;

public class RunMLP : MonoBehaviour
{
    public NNModel mlpModel;  // Drag & drop ONNX model in Inspector
    private Model runtimeModel;
    private IWorker worker;
    public Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        /*
        // Load the ONNX model into Barracuda
        runtimeModel = ModelLoader.Load(mlpModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);

        // Create dummy input tensor (batch size=1, input_dim=6)
        Tensor inputTensor = new Tensor(1, 6, new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f });

        // Execute the model
        worker.Execute(inputTensor);

        // Get output tensor
        Tensor outputTensor = worker.PeekOutput("output");

        float red = outputTensor[0, 0, 0, 0];   // R
        float green = outputTensor[0, 0, 0, 1]; // G
        float blue = outputTensor[0, 0, 0, 2];  // B


        Vector3 new_color = new Vector3(red, green, blue);
        // Log the RGB values
        Debug.Log("RGB Output: [" + red + ", " + green + ", " + blue + "]");

        // Cleanup
        inputTensor.Dispose();
        outputTensor.Dispose();
        worker.Dispose();
        */
    }

    public void predict_new_color(float[] first_and_2nd_dominant_color)
    {

        Debug.Log("first and 2nd domin color from RunMLP " + first_and_2nd_dominant_color);
        // Load the ONNX model into Barracuda
        runtimeModel = ModelLoader.Load(mlpModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);

        // Create dummy input tensor (batch size=1, input_dim=6)
        Tensor inputTensor = new Tensor(1, 6, first_and_2nd_dominant_color);

        // Execute the model
        worker.Execute(inputTensor);

        // Get output tensor
        Tensor outputTensor = worker.PeekOutput("output");

        float red = outputTensor[0, 0, 0, 0];   // R
        float green = outputTensor[0, 0, 0, 1]; // G
        float blue = outputTensor[0, 0, 0, 2];  // B


        Vector3 new_color = new Vector3(red, green, blue);
        // Log the RGB values
        Debug.Log("RGB Output: [" + red + ", " + green + ", " + blue + "]");

        // Clamp values to avoid issues
        red = Mathf.Clamp01(red);
        green = Mathf.Clamp01(green);
        blue = Mathf.Clamp01(blue);

        // Apply color to material
        Color newColor = new Color(red, green, blue, 1.0f);
        Debug.Log("Applying color: " + newColor);

        
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("No Renderer found on the object!");
        }


        // Cleanup
        inputTensor.Dispose();
        outputTensor.Dispose();
        worker.Dispose();
    }
}
