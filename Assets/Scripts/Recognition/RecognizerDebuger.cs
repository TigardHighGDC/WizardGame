using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizerDebuger : MonoBehaviour
{
    public static RecognizerDebuger Instance;
    public GameObject Line;
    public GameObject Corner;

    public void Awake()
    {
        Instance = this;
    }

    public void CornerDebuger(Vector3 cornerPoint)
    {
        Instantiate(Corner, cornerPoint, Quaternion.identity);
    }

    public void DeleteDebug()
    {
        GameObject[] corners = GameObject.FindGameObjectsWithTag("Debug");
        foreach (GameObject corner in corners)
        {
            Destroy(corner);
        }
    }

    public void DrawLine(float[] lineEquation)
    {
        Vector3 startPoint = new Vector3(0, -5, 0);
        Vector3 endPoint = new Vector3(0, 5, 0);

        startPoint.x = (startPoint.y - lineEquation[1]) / lineEquation[0];
        endPoint.x = (endPoint.y - lineEquation[1]) / lineEquation[0];

        GameObject line = Instantiate(Line, Vector3.zero, Quaternion.identity);

        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { startPoint, endPoint });
    }
}
