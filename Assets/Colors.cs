using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    /*
    public Color32 AverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < total; i++)
        {
            r += texColors *.r;
            g += texColors *.g;
            b += texColors *.b;
        }

        return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 255);
    }
    */

    public Color32 MainColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        List<colNum> colNumList = new List<colNum>();

        for (int i = 0; i < total; i++)
        {
            if (colNumList.Count > 0)
            {
                for (int j = 0; j < colNumList.Count; j++)
                {
                    if (colNumList[j].col.Equals(texColors)) colNumList[j].quantity += 1;
                    else AddColNumToList(texColors[j], colNumList);
                }
            }
            else AddColNumToList(texColors[i], colNumList);
        }

        Color32 col = new Color32();
        int quan = 0;

        foreach (colNum cn in colNumList)
        {
            if (cn.quantity > quan)
            {
                col = cn.col;
                quan = cn.quantity;
            }
        }

        col.a = 255;

        return col;

    }

    
    private void AddColNumToList(Color32 col, List<colNum> li)
    {
        colNum cn = new colNum();
        cn.col = col;
        cn.quantity = 1;
        li.Add(cn);
    }
    
}

public class colNum
{
    public Color32 col;
    public int quantity;
}