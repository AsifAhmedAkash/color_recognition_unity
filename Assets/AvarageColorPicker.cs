using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvarageColorPicker : MonoBehaviour
{
    [SerializeField] private Color32 objColor;
    // Start is called before the first frame update
    void Start()
    {
        objColor = gameObject.GetComponent<MeshRenderer>().material.color;
        Debug.Log("obj" + objColor);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(objColor.r + " " + objColor.g + " " + objColor.b + " ");
    }
    
}
