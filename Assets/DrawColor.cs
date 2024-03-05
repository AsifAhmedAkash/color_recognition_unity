using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawColor : MonoBehaviour
{
    [SerializeField] private RenderTexture rTex;
    [SerializeField] private Texture2D tex2d;
    [SerializeField] private RawImage rImg;
    [SerializeField] private Colors co;

    private Color32 col;

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    void Start()
    {
        tex2d = toTexture2D(rTex);
        //col = co.AverageColorFromTexture(tex2d);
        col = co.MainColorFromTexture(tex2d);
        rImg.color = col;
    }
}