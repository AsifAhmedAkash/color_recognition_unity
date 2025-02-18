using UnityEngine;
using Unity.Barracuda;
using UnityEngine.InputSystem;

public class RunMLP : MonoBehaviour
{
    public NNModel mlpModel;  // Drag & drop ONNX model in Inspector
    private Model runtimeModel;
    private IWorker worker;

    void Start()
    {
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

        // Log the RGB values
        Debug.Log("RGB Output: [" + red + ", " + green + ", " + blue + "]");

        // Cleanup
        inputTensor.Dispose();
        outputTensor.Dispose();
        worker.Dispose();
    }
}
