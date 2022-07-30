using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateParameters : MonoBehaviour
{
    private GameObject ATC, Bed, Chuck, Column, HeadStock, LM_Guide, Saddle, Saddle_Cover, Spindle, Table;
    public static float Scaling_Bed_X = 200.0f;
    public static float Scaling_Bed_Y = 35.0f;
    public static float Scaling_Bed_Z = 200.0f;
    public static float Scaling_SaddleCover_X = 100.0f;
    public static float Scaling_SaddleCover_Y = 40.0f;
    public static float Scaling_SaddleCover_Z = 200.0f;
    public static float Scaling_Saddle_X = 300.0f;
    public static float Scaling_Saddle_Y = 25.0f;
    public static float Scaling_Saddle_Z = 100.0f;
    public static float Scaling_Table_X = 200.0f;
    public static float Scaling_Table_Y = 50.0f;
    public static float Scaling_Table_Z = 100.0f;
    public static float Scaling_Column_X = 10.0f;
    public static float Scaling_Column_Y = 10.0f;
    public static float Scaling_Column_Z = 10.0f;
    public static float Scaling_HeadStock_X = 10.0f;
    public static float Scaling_HeadStock_Y = 10.0f;
    public static float Scaling_HeadStock_Z = 10.0f;
    public static float Scaling_Chuck_D = 10.0f;
    public static float Scaling_Chuck_L = 100.0f;
    public static float Scaling_Spindle_D = 10.0f;
    public static float Scaling_Spindle_L = 20.0f;
    public static float SaddleCover_Z_term = 0.5f;
    public static float Saddle_Z_term = 3.0f;
    public static float Table_Z_term = 3.0f;
    public static float Bed_Z_Location_Scaling = 10.0f;
    public static float ATC_X_Location_Scaling = 5.0f;
    public static float ATC_Y_Location_Scaling = 3.8f;
    public static float LM_Guide_Y_Location_Scaling = 8.5f;
    public static float SaddleCover_Y_Location_Scaling = 2.5f;
    public static float Saddle_Y_Location_Scaling = 2.5f;
    [SerializeField] Text timeText;
    [SerializeField] Text ATCText, BedText, ColumnText, HeadStockText, KneeText, TableText, ToolHolderText, SaddleText, LMGuideText, SpindleText;

    // Start is called before the first frame update
    void Start()
    {
        ATC = GameObject.Find("MCT_ATC");
        Bed = GameObject.Find("MCT_Bed");
        Chuck = GameObject.Find("MCT_Chuck");
        Column = GameObject.Find("MCT_Column");
        HeadStock = GameObject.Find("MCT_HeadStock");
        Saddle = GameObject.Find("MCT_Saddle");
        Saddle_Cover = GameObject.Find("MCT_Saddle_Cover");
        Table = GameObject.Find("MCT_Table");
        Spindle = GameObject.Find("MCT_Spindle");
        LM_Guide = GameObject.Find("MCT_LM_Guide");
    }

    // Update is called once per frame
    void Update()
    {
        Bed.transform.position = new Vector3(0, 0, 0);
        Saddle_Cover.transform.position = new Vector3(0, Bed.transform.localScale.y * Scaling_Bed_Y * 0.1f, SaddleCover_Z_term);
        Saddle.transform.position = new Vector3(0, 0.1f * (Bed.transform.localScale.y * Scaling_Bed_Y + Saddle_Cover.transform.localScale.y * Scaling_Saddle_Y), -Saddle_Z_term);
        Table.transform.position = new Vector3(0, 0.1f * (Bed.transform.localScale.y * Scaling_Bed_Y + Saddle_Cover.transform.localScale.y * Scaling_Saddle_Y
            + Saddle.transform.localScale.y * Scaling_Table_Y / 2), -Table_Z_term);
        Column.transform.position = new Vector3(0, Column.transform.localScale.y * 0.5f,
            SaddleCover_Z_term + Bed.transform.localScale.z * Bed_Z_Location_Scaling + Column.transform.localScale.z * 0.5f);
        HeadStock.transform.position = new Vector3(0, Column.transform.localScale.y - HeadStock.transform.localScale.y * 0.5f,
            SaddleCover_Z_term + Bed.transform.localScale.z * Bed_Z_Location_Scaling - HeadStock.transform.localScale.z * 0.5f);
        Chuck.transform.position = new Vector3(0, Column.transform.localScale.y - HeadStock.transform.localScale.y - Chuck.transform.localScale.y,
            SaddleCover_Z_term + Bed.transform.localScale.z * Bed_Z_Location_Scaling - HeadStock.transform.localScale.z * 0.5f);
        Spindle.transform.position = new Vector3(0, Column.transform.localScale.y - HeadStock.transform.localScale.y - Chuck.transform.localScale.y * 2.0f 
            - Spindle.transform.localScale.y, SaddleCover_Z_term + Bed.transform.localScale.z * Bed_Z_Location_Scaling - HeadStock.transform.localScale.z * 0.5f);
        ATC.transform.position = new Vector3(-HeadStock.transform.localScale.x * 0.5f , Column.transform.localScale.y,
            SaddleCover_Z_term + Bed.transform.localScale.z * Bed_Z_Location_Scaling - HeadStock.transform.localScale.z * 0.5f);
        LM_Guide.transform.position = new Vector3(0, Bed.transform.localScale.y * Scaling_Bed_Y * 0.1f + Saddle_Cover.transform.localScale.y * SaddleCover_Y_Location_Scaling
            + Saddle.transform.localScale.y * Saddle_Y_Location_Scaling - LM_Guide_Y_Location_Scaling, 0);
    }

    public void ClickApplyButton()
    {
        Bed.transform.localScale 
            = new Vector3(Bed_Slider_X.SDValue / Scaling_Bed_X, Bed_Slider_Y.SDValue / Scaling_Bed_Y, Bed_Slider_Z.SDValue / Scaling_Bed_Z);
        Chuck.transform.localScale 
            = new Vector3(Chuck_Slider_D.SDValue / Scaling_Chuck_D, Chuck_Slider_L.SDValue / Scaling_Chuck_L, Chuck_Slider_D.SDValue / Scaling_Chuck_D);
        Column.transform.localScale 
            = new Vector3(Column_Slider_X.SDValue / Scaling_Column_X, Column_Slider_Y.SDValue / Scaling_Column_Y, Column_Slider_Z.SDValue / Scaling_Column_Z);
        HeadStock.transform.localScale 
            = new Vector3(HeadStock_Slider_X.SDValue / Scaling_HeadStock_X, HeadStock_Slider_Y.SDValue / Scaling_HeadStock_Y, HeadStock_Slider_Z.SDValue / Scaling_HeadStock_Z);
        Saddle.transform.localScale
            = new Vector3(Saddle_Slider_X.SDValue / Scaling_Saddle_X, Saddle_Slider_Y.SDValue / Scaling_Saddle_Y, Saddle_Slider_Z.SDValue / Scaling_Saddle_Z);
        Saddle_Cover.transform.localScale
            = new Vector3(SaddleCover_Slider_X.SDValue / Scaling_SaddleCover_X, SaddleCover_Slider_Y.SDValue / Scaling_SaddleCover_Y, SaddleCover_Slider_Z.SDValue / Scaling_SaddleCover_Z);
        Table.transform.localScale
            = new Vector3(Table_Slider_X.SDValue / Scaling_Table_X, Table_Slider_Y.SDValue / Scaling_Table_Y, Table_Slider_Z.SDValue / Scaling_Table_Z);

        timeText.text = string.Format("Delay Time = {0:0.0}ms", Time.deltaTime * 1000.0f);

        BedText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_Bed_X * Bed.transform.localScale.x, Scaling_Bed_Y * Bed.transform.localScale.y, Scaling_Bed_Z * Bed.transform.localScale.z);
        ColumnText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_Column_X * Column.transform.localScale.x, Scaling_Column_Y * Column.transform.localScale.y, Scaling_Column_Z * Column.transform.localScale.z);
        HeadStockText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_HeadStock_X * HeadStock.transform.localScale.x, Scaling_HeadStock_Y * HeadStock.transform.localScale.y, Scaling_HeadStock_Z * HeadStock.transform.localScale.z);
        KneeText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_SaddleCover_X * Saddle_Cover.transform.localScale.x, Scaling_SaddleCover_Y * Saddle_Cover.transform.localScale.y, Scaling_SaddleCover_Z * Saddle_Cover.transform.localScale.z);
        TableText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_Table_X * Table.transform.localScale.x, Scaling_Table_Y * Table.transform.localScale.y, Scaling_Table_Z * Table.transform.localScale.z);
        SaddleText.text = string.Format("{0:0.0} / {1:0.0} / {2:0.0}",
            Scaling_Saddle_X * Saddle.transform.localScale.x, Scaling_Saddle_Y * Saddle.transform.localScale.y, Scaling_Saddle_Z * Saddle.transform.localScale.z);
        ToolHolderText.text = string.Format("{0:0.0} / {1:0.0}",
            Scaling_Chuck_D * Chuck.transform.localScale.x, Scaling_Chuck_L * Chuck.transform.localScale.y);
    }
}
