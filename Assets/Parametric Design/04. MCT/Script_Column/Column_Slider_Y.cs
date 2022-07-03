using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Column_Slider_Y : MonoBehaviour
{
    public Text ValueText;
    public static int SDValue = 250;

    private void Start()
    {
        
    }
    public void Update()
    {
        ValueText = GetComponent<Text>();
    }

    public void valueUpdate(float value)
    {
        ValueText.text = Mathf.RoundToInt(value) + "cm";
        SDValue = Mathf.RoundToInt(value);
        //Debug.Log(SDValue);
    }
}
