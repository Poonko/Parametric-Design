using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateMeasuringMachine : MonoBehaviour
{

    private Text Viewer;
    private GameObject Ceiling, Left_Column, Right_Column, Spindle;
    private static int Value;
    private float unit = 1000.0f;
    private float half_unit = 2000.0f;
    /*private float Right_Column_Spindle_Ratio = 2.0f;
    private float Spindle_Ceiling_Minimum_Distance = 2.0f;
    private float Ceiling_Right_Column_Minimum_Distance = 1.0f;*/
    private float Maximum_Spindle_Length = 2.0f;
    private float Maximum_Spindle_Diameter = 1.0f;

    void Start()
    {
        Viewer = GetComponent<Text>();
        //Viewer.text = "0";
        Value = 0;
        Ceiling = GameObject.Find("Ceiling");
        Left_Column = GameObject.Find("Left_Column");
        Right_Column = GameObject.Find("Right_Column");
        Spindle = GameObject.Find("Spindle");
    }

    void Update()
    {
        Ceiling.transform.position = new Vector3(Ceiling.transform.position.x,
            Left_Column.transform.localScale.y + Ceiling.transform.localScale.y / 2, Ceiling.transform.position.z);
        Left_Column.transform.position = new Vector3((Left_Column.transform.localScale.x - Ceiling.transform.localScale.x) / 2,
                    Left_Column.transform.position.y, Left_Column.transform.position.z);
        Right_Column.transform.position = new Vector3((Ceiling.transform.localScale.x - Right_Column.transform.localScale.x) / 2,
                    Right_Column.transform.position.y, Right_Column.transform.position.z);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x,
                    Left_Column.transform.localScale.y - Spindle.transform.localScale.y, Spindle.transform.position.z);

        
        /*Left_Column.transform.position = new Vector3(Left_Column.transform.position.x, Left_Column.transform.position.y, 
            (Ceiling.transform.localScale.z + Left_Column.transform.localScale.z) / 2.0f);
        Right_Column.transform.position = new Vector3(Right_Column.transform.position.x,
            Left_Column.transform.localScale.y - Right_Column.transform.localScale.y / 2.0f, Right_Column.transform.position.z);
        Right_Column.transform.position = new Vector3(Right_Column.transform.position.x, Right_Column.transform.position.y,
            (Ceiling.transform.localScale.z - Right_Column.transform.localScale.z) / 2.0f);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x,
            Left_Column.transform.localScale.y - Right_Column.transform.localScale.y - Spindle.transform.localScale.y,
            Ceiling.transform.localScale.z / 2.0f - Right_Column.transform.localScale.z * 0.75f);

        if (Ceiling.transform.localScale.x < Left_Column.transform.localScale.x)
        {
            Ceiling.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Ceiling.transform.localScale.y, Ceiling.transform.localScale.z);
        }*/

        if (Left_Column.transform.localScale.x > Ceiling.transform.localScale.x * 1.0f / 3.0f)
        {
            Left_Column.transform.localScale = new Vector3(Ceiling.transform.localScale.x * 1.0f / 3.0f, 
                Left_Column.transform.localScale.y, Left_Column.transform.localScale.z);
        }

        if (Right_Column.transform.localScale.x > Ceiling.transform.localScale.x * 1.0f / 3.0f)
        {
            Right_Column.transform.localScale = new Vector3(Ceiling.transform.localScale.x * 1.0f / 3.0f,
                Right_Column.transform.localScale.y, Right_Column.transform.localScale.z);
        }
        /*
        if (Left_Column.transform.localScale.y < Right_Column.transform.localScale.y + Ceiling.transform.localScale.y + Maximum_Spindle_Length)
        {
            Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Spindle_Ceiling_Minimum_Distance + Right_Column.transform.localScale.y +
                Ceiling.transform.localScale.y + Spindle.transform.localScale.y, Left_Column.transform.localScale.z);
            Left_Column.transform.position = new Vector3(Left_Column.transform.position.x, Left_Column.transform.localScale.y / 2.0f, Left_Column.transform.position.z);
        }

        if (Ceiling.transform.localScale.z < Right_Column.transform.localScale.z)
        {
            Ceiling.transform.localScale = new Vector3(Ceiling.transform.localScale.x, Ceiling.transform.localScale.y,
                Right_Column.transform.localScale.z + Ceiling_Right_Column_Minimum_Distance);
        }*/

        if (Spindle.transform.localScale.y >= Maximum_Spindle_Length)
        {
            Spindle.transform.localScale = new Vector3(Spindle.transform.localScale.x, Maximum_Spindle_Length, Spindle.transform.localScale.z);
        }
    }

    public void Getvalue(string value) //숫자 입력
    {
        if (Viewer.text == "0")
        {
            Viewer.text = value;
        }
        else if (Viewer.text == "0" && value == "0")
        {
            Viewer.text = value;
        }
        else if (value == "Enter")
        {
            ConvertValueIntoNumber();
            Viewer.text = "0";
        }
        else if (value == "BackSpace")
        {
            Viewer.text = Viewer.text.Substring(0, Viewer.text.Length - 1);
        }
        else
        {
            Viewer.text += value;
        }
    }

    public int ConvertValueIntoNumber() //문자열로 입력된 숫자를 정수형으로 바꿈, Value 반환
    {
        int localvalue = Convert.ToInt32(Viewer.text);
        return Value = localvalue;
    }

    public void CeilingXAxisChange()
    {
        Ceiling.transform.localScale = new Vector3(Value / unit, Ceiling.transform.localScale.y, Ceiling.transform.localScale.z);
    }

    public void CeilingYAxisChange()
    {
        Ceiling.transform.localScale = new Vector3(Ceiling.transform.localScale.x, Value / unit, Ceiling.transform.localScale.z);
        Ceiling.transform.position = new Vector3(Ceiling.transform.position.x, 
            Left_Column.transform.localScale.y + Ceiling.transform.localScale.y / 2, Ceiling.transform.position.z);
    }

    public void CeilingZAxisChange()
    {
        Ceiling.transform.localScale = new Vector3(Ceiling.transform.localScale.x, Ceiling.transform.localScale.y, Value / unit);
    }

    public void Left_ColumnXAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Value / unit, Left_Column.transform.localScale.y, Left_Column.transform.localScale.z);
        Left_Column.transform.position = new Vector3((Left_Column.transform.localScale.x - Ceiling.transform.localScale.x) / 2,
            Left_Column.transform.position.y, Left_Column.transform.position.z);
    }

    public void Left_ColumnYAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Value / unit, Left_Column.transform.localScale.z);
        Left_Column.transform.position = new Vector3(Left_Column.transform.position.x, Value / half_unit, Left_Column.transform.position.z);
    }

    public void Left_ColumnZAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Left_Column.transform.localScale.y, Value / unit);
    }

    public void Right_ColumnXAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Value / unit, Right_Column.transform.localScale.y, Right_Column.transform.localScale.z);
        Right_Column.transform.position = new Vector3((Ceiling.transform.localScale.x - Right_Column.transform.localScale.x) / 2,
            Right_Column.transform.position.y, Right_Column.transform.position.z);
    }

    public void Right_ColumnYAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, Value / unit, Right_Column.transform.localScale.z);
        Right_Column.transform.position = new Vector3(Right_Column.transform.position.x, Value / half_unit, Right_Column.transform.position.z);
    }

    public void Right_ColumnZAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, Right_Column.transform.localScale.y, Value / unit);
    }

    public void SpindleYAxisChange()
    {
        Spindle.transform.localScale = new Vector3(Spindle.transform.localScale.x, Value / unit, Spindle.transform.localScale.z);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x,
            Left_Column.transform.localScale.y - Spindle.transform.localScale.y, Spindle.transform.position.z);
    }

    public void SpindleRAxisChange()
    {
        if (Value / unit > Maximum_Spindle_Diameter)
        {
            Spindle.transform.localScale = new Vector3(Maximum_Spindle_Diameter, Spindle.transform.localScale.y, Maximum_Spindle_Diameter);
        }
        else
        {
            Spindle.transform.localScale = new Vector3(Value / unit, Spindle.transform.localScale.y, Value / unit);
        }
    }
}