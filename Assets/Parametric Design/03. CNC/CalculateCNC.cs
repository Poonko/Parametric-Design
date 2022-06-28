using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateCNC : MonoBehaviour
{

    private Text Viewer;
    private GameObject Bed, Left_Column, Right_Column, Chuck, TailStock;
    private static int Value;
    private float unit = 1000.0f;
    private float half_unit = 2000.0f;
    private float Chuck_TailStock_Minimum_Distance = 4.0f;
    private float Maximum_Chuck_Length = 1.0f;
    private float Maximum_Chuck_Diameter = 1.0f;
    private float Maximum_TailStock_Length = 1.0f;
    private float Maximum_TailStock_Diameter = 0.5f;

    void Start()
    {
        Viewer = GetComponent<Text>();
        Value = 0;
        Bed = GameObject.Find("Bed");
        Left_Column = GameObject.Find("Left_Column");
        Right_Column = GameObject.Find("Right_Column");
        Chuck = GameObject.Find("Chuck");
        TailStock = GameObject.Find("TailStock");
    }

    void Update()
    {
        Bed.transform.position = new Vector3(Bed.transform.position.x, Bed.transform.localScale.y / 2, Bed.transform.position.z);
        Left_Column.transform.position = new Vector3((Left_Column.transform.localScale.x - Bed.transform.localScale.x) / 2,
                    Bed.transform.localScale.y + Left_Column.transform.localScale.y / 2, Left_Column.transform.position.z);
        Right_Column.transform.position = new Vector3((Bed.transform.localScale.x - Right_Column.transform.localScale.x) / 2,
                    Bed.transform.localScale.y + Right_Column.transform.localScale.y / 2, Right_Column.transform.position.z);
        Chuck.transform.position = new Vector3(-Bed.transform.localScale.x / 2 + Left_Column.transform.localScale.x + Chuck.transform.localScale.y,
                    Bed.transform.localScale.y + Left_Column.transform.localScale.y / 2, Chuck.transform.position.z);
        TailStock.transform.position = new Vector3(Bed.transform.localScale.x / 2 - Right_Column.transform.localScale.x - TailStock.transform.localScale.y,
                    Bed.transform.localScale.y + Right_Column.transform.localScale.y / 2, TailStock.transform.position.z);

        if (Bed.transform.localScale.x <= 
            Left_Column.transform.localScale.x + Right_Column.transform.localScale.x + Maximum_Chuck_Length + Maximum_TailStock_Length)
        {
            Bed.transform.localScale = new Vector3(Left_Column.transform.localScale.x + Right_Column.transform.localScale.x + Maximum_Chuck_Length
                + Maximum_TailStock_Length + Chuck_TailStock_Minimum_Distance, Bed.transform.localScale.y, Bed.transform.localScale.z);
        }

        if (Bed.transform.localScale.z <= Left_Column.transform.localScale.z)
        {
            Bed.transform.localScale = new Vector3(Bed.transform.localScale.x, Bed.transform.localScale.y, Left_Column.transform.localScale.z);
        }

        if (Bed.transform.localScale.z <= Right_Column.transform.localScale.z)
        {
            Bed.transform.localScale = new Vector3(Bed.transform.localScale.x, Bed.transform.localScale.y, Right_Column.transform.localScale.z);
        }

        if (Chuck.transform.localScale.y > Maximum_Chuck_Length)
        {
            Chuck.transform.localScale = new Vector3(Chuck.transform.localScale.x, Maximum_Chuck_Length, Chuck.transform.localScale.z);
        }

        if (TailStock.transform.localScale.y > Maximum_TailStock_Length)
        {
            TailStock.transform.localScale = new Vector3(TailStock.transform.localScale.x, Maximum_TailStock_Length, TailStock.transform.localScale.z);
        }

        if (Left_Column.transform.localScale.y < 2.0f * Chuck.transform.localScale.x)
        {
            Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, 2.0f * Chuck.transform.localScale.x, Left_Column.transform.localScale.z);
        }

        if (Left_Column.transform.localScale.z < 2.0f * Chuck.transform.localScale.z)
        {
            Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Left_Column.transform.localScale.y, 2.0f * Chuck.transform.localScale.z);
        }

        if (Right_Column.transform.localScale.y < 2.0f * TailStock.transform.localScale.x)
        {
            Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, 2.0f * Chuck.transform.localScale.x, Right_Column.transform.localScale.z);
        }

        if (Right_Column.transform.localScale.z < 2.0f * TailStock.transform.localScale.z)
        {
            Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, Right_Column.transform.localScale.y, 2.0f * Chuck.transform.localScale.z);
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

    public void BedXAxisChange()
    {
        Bed.transform.localScale = new Vector3(Value / unit, Bed.transform.localScale.y, Bed.transform.localScale.z);
    }

    public void BedYAxisChange()
    {
        Bed.transform.localScale = new Vector3(Bed.transform.localScale.x, Value / unit, Bed.transform.localScale.z);
        Bed.transform.position = new Vector3(Bed.transform.position.x, Bed.transform.localScale.y / 2, Bed.transform.position.z);
    }

    public void BedZAxisChange()
    {
        Bed.transform.localScale = new Vector3(Bed.transform.localScale.x, Bed.transform.localScale.y, Value / unit);
    }

    public void Left_ColumnXAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Value / unit, Left_Column.transform.localScale.y, Left_Column.transform.localScale.z);
        Left_Column.transform.position = new Vector3((Left_Column.transform.localScale.x - Bed.transform.localScale.x) / 2,
            Left_Column.transform.position.y, Left_Column.transform.position.z);
    }

    public void Left_ColumnYAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Value / unit, Left_Column.transform.localScale.z);
        Left_Column.transform.position = new Vector3(Left_Column.transform.position.x, Value / half_unit + Bed.transform.localScale.y, Left_Column.transform.position.z);
    }

    public void Left_ColumnZAxisChange()
    {
        Left_Column.transform.localScale = new Vector3(Left_Column.transform.localScale.x, Left_Column.transform.localScale.y, Value / unit);
    }

    public void Right_ColumnXAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Value / unit, Right_Column.transform.localScale.y, Right_Column.transform.localScale.z);
        Right_Column.transform.position = new Vector3((Bed.transform.localScale.x - Right_Column.transform.localScale.x) / 2,
            Right_Column.transform.position.y, Right_Column.transform.position.z);
    }

    public void Right_ColumnYAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, Value / unit, Right_Column.transform.localScale.z);
        Right_Column.transform.position = new Vector3(Right_Column.transform.position.x, Value / half_unit + Bed.transform.localScale.y, Right_Column.transform.position.z);
    }

    public void Right_ColumnZAxisChange()
    {
        Right_Column.transform.localScale = new Vector3(Right_Column.transform.localScale.x, Right_Column.transform.localScale.y, Value / unit);
    }

    public void ChuckXAxisChange()
    {
        Chuck.transform.localScale = new Vector3(Chuck.transform.localScale.x, Value / unit, Chuck.transform.localScale.z);
        Chuck.transform.position = new Vector3(-Bed.transform.localScale.x / 2 + Left_Column.transform.localScale.x + Chuck.transform.localScale.y,
            Bed.transform.localScale.y + Left_Column.transform.localScale.y / 2, Chuck.transform.position.z);
    }

    public void ChuckRAxisChange()
    {
        if (Value / unit > Maximum_Chuck_Diameter)
        {
            Chuck.transform.localScale = new Vector3(Maximum_Chuck_Diameter, Chuck.transform.localScale.y, Maximum_Chuck_Diameter);
        }
        else
        {
            Chuck.transform.localScale = new Vector3(Value / unit, Chuck.transform.localScale.y, Value / unit);
        }
    }

    public void TailStockXAxisChange()
    {
        TailStock.transform.localScale = new Vector3(TailStock.transform.localScale.x, Value / unit, TailStock.transform.localScale.z);
        TailStock.transform.position = new Vector3(Bed.transform.localScale.x / 2 - Right_Column.transform.localScale.x - TailStock.transform.localScale.y,
            Bed.transform.localScale.y + Right_Column.transform.localScale.y / 2, TailStock.transform.position.z);
    }

    public void TailStockRAxisChange()
    {
        if (Value / unit > Maximum_TailStock_Diameter)
        {
            TailStock.transform.localScale = new Vector3(Maximum_TailStock_Diameter, TailStock.transform.localScale.y, Maximum_TailStock_Diameter);
        }
        else
        {
            TailStock.transform.localScale = new Vector3(Value / unit, TailStock.transform.localScale.y, Value / unit);
        }
    }
}