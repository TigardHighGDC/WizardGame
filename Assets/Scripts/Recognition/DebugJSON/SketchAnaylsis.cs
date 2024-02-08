using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SketchAnaylsis : MonoBehaviour
{
    public string fileName = "";

    void Start()
    {
        string filePath = Application.dataPath + "/" + fileName + ".json";
        List<Vector3[]> sketchList;

        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            sketchList = JsonConvert.DeserializeObject<List<Vector3[]>>(fileContents);
        }
        else
        {
            return;
        }

        List<float> lineDeviationList = new List<float>();
        List<float> circleDeviationList = new List<float>();
        List<float> arcSizeList = new List<float>();
        List<float> listDCR = new List<float>();
        List<float> listNDDE = new List<float>();

        foreach (Vector3[] points in sketchList)
        {
            float[] directionSlice = PreRecognition.DirectionChangeCalculator(points);
            lineDeviationList.Add(ShapeRecognition.LineDeviation(points));
            float[] circle = ShapeRecognition.CircleDeviation(points);
            circleDeviationList.Add(circle[0]);
            arcSizeList.Add(circle[1]);
            listDCR.Add(PreRecognition.DCR_Calculator(directionSlice));
            listNDDE.Add(PreRecognition.NDDE_Calculator(directionSlice, points));
        }

        lineDeviationList.Sort();
        circleDeviationList.Sort();
        arcSizeList.Sort();
        listDCR.Sort();
        listNDDE.Sort();

        Debug.Log("Line Deviation -> " + QuartileRange(lineDeviationList));
        Debug.Log("Circle Deviation -> " + QuartileRange(circleDeviationList));
        Debug.Log("Arc Size -> " + QuartileRange(arcSizeList));
        Debug.Log("DCR -> " + QuartileRange(listDCR));
        Debug.Log("NDDE -> " + QuartileRange(listNDDE));
    }

    public string StandardDeviation(List<float> list)
    {
        float sum = 0;
        foreach (float f in list)
        {
            sum += f;
        }
        float average = sum / list.Count;
        float variance = 0;
        foreach (float f in list)
        {
            variance += (f - average) * (f - average);
        }
        variance /= list.Count;
        float standardDeviation = Mathf.Sqrt(variance);
        return "Average: " + average + " Standard Deviation: " + standardDeviation;
    }

    public string QuartileRange(List<float> list)
    {
        float q1 = list[list.Count / 4];
        float q2 = list[list.Count / 2];
        float q3 = list[list.Count * 3 / 4];
        return "Q1: " + q1 + " Q2: " + q2 + " Q3: " + q3;
    }
}
