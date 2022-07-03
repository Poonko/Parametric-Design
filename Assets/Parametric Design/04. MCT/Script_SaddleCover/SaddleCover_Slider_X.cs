﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaddleCover_Slider_X : MonoBehaviour
{
    public Text ValueText;
    public static int SDValue = 100;

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
