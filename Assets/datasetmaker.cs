using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class datasetmaker : MonoBehaviour
{
    public Camera targetCamera;   // Assign your camera here (or it will use Camera.main)
    public GameObject targetObject;  // The GameObject to toggle
    private int orderNumber = 0;  // Counter for naming

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CaptureSequence());
        }
    }

    IEnumerator CaptureSequence()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        // Disable object
        targetObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        yield return CaptureScreenshot("dvm_" + orderNumber);

        // Enable object
        targetObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        yield return CaptureScreenshot("dv_" + orderNumber);

        orderNumber++;
    }

    IEnumerator CaptureScreenshot(string fileName)
    {
        // Create RenderTexture
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        targetCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        targetCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Save file
        byte[] bytes = screenShot.EncodeToPNG();
        string filePath = Path.Combine(Application.dataPath, fileName + ".png");
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Saved screenshot: " + filePath);

        yield return null;
    }
}
