using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateMCT : MonoBehaviour
{

    private Text Viewer;
    /*private Button Button_Table_X, Button_Table_Y, Button_Table_Z, Button_Column_X, Button_Column_Y, Button_Column_Z,
        Button_HeadStock_X, Button_HeadStock_Y, Button_HeadStock_Z, Button_Spindle_X, Button_Spindle_Y, Button_Spindle_Z;*/
    private GameObject Table, Column, HeadStock, Spindle;
    private static int Value;
    private float unit = 1000.0f;
    private float half_unit = 2000.0f;
    private float HeadStock_Spindle_Ratio = 2.0f;
    private float Spindle_Table_Minimum_Distance = 2.0f;
    private float Table_HeadStock_Minimum_Distance = 1.0f;
    private float Maximum_Spindle_Length = 0.5f;
    private float Maximum_Spindle_Diameter = 0.5f;

    void Start()
    {
        Viewer = GetComponent<Text>();
        //Viewer.text = "0";
        Value = 0;
        /*Button_Table_X = GameObject.Find("Table X").GetComponent<Button>();
        Button_Table_Y = GameObject.Find("Table Y").GetComponent<Button>();
        Button_Table_Z = GameObject.Find("Table Z").GetComponent<Button>();
        Button_Column_X = GameObject.Find("Column X").GetComponent<Button>();
        Button_Column_Y = GameObject.Find("Column Y").GetComponent<Button>();
        Button_Column_Z = GameObject.Find("Column Z").GetComponent<Button>();
        Button_HeadStock_X = GameObject.Find("HeadStock X").GetComponent<Button>();
        Button_HeadStock_Y = GameObject.Find("HeadStock Y").GetComponent<Button>();
        Button_HeadStock_Z = GameObject.Find("HeadStock Z").GetComponent<Button>();
        Button_Spindle_X = GameObject.Find("Spindle X").GetComponent<Button>();
        Button_Spindle_Y = GameObject.Find("Spindle Y").GetComponent<Button>();
        Button_Spindle_Z = GameObject.Find("Spindle Z").GetComponent<Button>();*/
        Table = GameObject.Find("Table");
        Column = GameObject.Find("Column");
        HeadStock = GameObject.Find("HeadStock");
        Spindle = GameObject.Find("Spindle");
    }

    void Update()
    {
        Column.transform.position = new Vector3(Column.transform.position.x, Column.transform.position.y,
            (Table.transform.localScale.z + Column.transform.localScale.z) / 2.0f);
        HeadStock.transform.position = new Vector3(HeadStock.transform.position.x,
            Column.transform.localScale.y - HeadStock.transform.localScale.y / 2.0f, HeadStock.transform.position.z);
        HeadStock.transform.position = new Vector3(HeadStock.transform.position.x, HeadStock.transform.position.y,
            (Table.transform.localScale.z - HeadStock.transform.localScale.z) / 2.0f);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x,
            Column.transform.localScale.y - HeadStock.transform.localScale.y - Spindle.transform.localScale.y, 
            Table.transform.localScale.z / 2.0f - HeadStock.transform.localScale.z * 0.75f);

        if (Table.transform.localScale.x < Column.transform.localScale.x)
        {
            Table.transform.localScale = new Vector3(Column.transform.localScale.x, Table.transform.localScale.y, Table.transform.localScale.z);
        }

        if (Column.transform.localScale.x < HeadStock.transform.localScale.x)
        {
            Column.transform.localScale = new Vector3(HeadStock.transform.localScale.x, Column.transform.localScale.y, Column.transform.localScale.z);
        }

        if (HeadStock.transform.localScale.x < HeadStock_Spindle_Ratio * Spindle.transform.localScale.x)
        {
            HeadStock.transform.localScale = new Vector3(HeadStock_Spindle_Ratio * Spindle.transform.localScale.x, 
                HeadStock.transform.localScale.y, HeadStock.transform.localScale.z);
        }

        if (Column.transform.localScale.y < HeadStock.transform.localScale.y + Table.transform.localScale.y + Maximum_Spindle_Length)
        {
            Column.transform.localScale = new Vector3(Column.transform.localScale.x, Spindle_Table_Minimum_Distance + HeadStock.transform.localScale.y + 
                Table.transform.localScale.y + Spindle.transform.localScale.y, Column.transform.localScale.z);
            Column.transform.position = new Vector3(Column.transform.position.x, Column.transform.localScale.y / 2.0f, Column.transform.position.z);
        }

        if (Table.transform.localScale.z < HeadStock.transform.localScale.z)
        {
            Table.transform.localScale = new Vector3(Table.transform.localScale.x, Table.transform.localScale.y, 
                HeadStock.transform.localScale.z + Table_HeadStock_Minimum_Distance);
        }

        if (Spindle.transform.localScale.y >= Maximum_Spindle_Length)
        {
            Spindle.transform.localScale = new Vector3(Spindle.transform.localScale.x, Maximum_Spindle_Length, Spindle.transform.localScale.z);
        }

        if (Spindle.transform.localScale.x >= Maximum_Spindle_Diameter || Spindle.transform.localScale.z >= Maximum_Spindle_Diameter)
        {
            Spindle.transform.localScale = new Vector3(Maximum_Spindle_Diameter, Spindle.transform.localScale.y, Maximum_Spindle_Diameter);
        }
    }

    public void Getvalue(string value) //텍스트 
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

    public int ConvertValueIntoNumber()
    {
        int localvalue = Convert.ToInt32(Viewer.text);
        return Value = localvalue;
    }

    public void TableXAxisChange()
    {
        Table.transform.localScale = new Vector3(Value / unit, Table.transform.localScale.y, Table.transform.localScale.z);
    }

    public void TableYAxisChange()
    {
        Table.transform.localScale = new Vector3(Table.transform.localScale.x, Value / unit, Table.transform.localScale.z);
        Table.transform.position = new Vector3(Table.transform.position.x, Value / half_unit, Table.transform.position.z);
    }

    public void TableZAxisChange()
    {
        Table.transform.localScale = new Vector3(Table.transform.localScale.x, Table.transform.localScale.y, Value / unit);
    }

    public void ColumnXAxisChange()
    {
        Column.transform.localScale = new Vector3(Value / unit, Column.transform.localScale.y, Column.transform.localScale.z);
    }

    public void ColumnYAxisChange()
    {
        Column.transform.localScale = new Vector3(Column.transform.localScale.x, Value / unit, Column.transform.localScale.z);
        Column.transform.position = new Vector3(Column.transform.position.x, Value / half_unit, Column.transform.position.z);
    }

    public void ColumnZAxisChange()
    {
        Column.transform.localScale = new Vector3(Column.transform.localScale.x, Column.transform.localScale.y, Value / unit);
        Column.transform.position = new Vector3(Column.transform.position.x, Column.transform.position.y,
            (Table.transform.localScale.z + Column.transform.localScale.z) / 2.0f);
    }

    public void HeadStockXAxisChange()
    {
        HeadStock.transform.localScale = new Vector3(Value / unit, HeadStock.transform.localScale.y, HeadStock.transform.localScale.z);
    }

    public void HeadStockYAxisChange()
    {
        HeadStock.transform.localScale = new Vector3(HeadStock.transform.localScale.x, Value / unit, HeadStock.transform.localScale.z);
        HeadStock.transform.position = new Vector3(HeadStock.transform.position.x,
            Column.transform.localScale.y - HeadStock.transform.localScale.y / 2.0f, HeadStock.transform.position.z);
    }

    public void HeadStockZAxisChange()
    {
        HeadStock.transform.localScale = new Vector3(HeadStock.transform.localScale.x, HeadStock.transform.localScale.y, Value / unit);
        HeadStock.transform.position = new Vector3(HeadStock.transform.position.x, HeadStock.transform.position.y,
            (Table.transform.localScale.z - HeadStock.transform.localScale.z) / 2.0f);
    }

    public void SpindleXAxisChange()
    {
        Spindle.transform.localScale = new Vector3(Value / unit, Spindle.transform.localScale.y, Spindle.transform.localScale.z);
    }

    public void SpindleYAxisChange()
    {
        Spindle.transform.localScale = new Vector3(Spindle.transform.localScale.x, Value / unit, Spindle.transform.localScale.z);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x,
            Column.transform.localScale.y - HeadStock.transform.localScale.y - Spindle.transform.localScale.y, Spindle.transform.position.z);
    }

    public void SpindleZAxisChange()
    {
        Spindle.transform.localScale = new Vector3(Spindle.transform.localScale.x, Spindle.transform.localScale.y, Value / unit);
        Spindle.transform.position = new Vector3(Spindle.transform.position.x, Spindle.transform.position.y,
            Table.transform.localScale.z / 2.0f - HeadStock.transform.localScale.z * 0.75f);
    }

    public void SpindleRAxisChange()
    {
        if (Value / unit > Maximum_Spindle_Diameter)
        {
            Spindle.transform.localScale = new Vector3(Maximum_Spindle_Diameter, Spindle.transform.localScale.y, Maximum_Spindle_Diameter);
            Spindle.transform.position = new Vector3(Spindle.transform.position.x, Spindle.transform.position.y,
                Table.transform.localScale.z / 2.0f - HeadStock.transform.localScale.z * 0.75f);
        }
        else
        {
            Spindle.transform.localScale = new Vector3(Value / unit, Spindle.transform.localScale.y, Value / unit);
            Spindle.transform.position = new Vector3(Spindle.transform.position.x, Spindle.transform.position.y,
                Table.transform.localScale.z / 2.0f - HeadStock.transform.localScale.z * 0.75f);
        }
    }
}