using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public bool FightMode = false;
    public GameObject debugObject;

    private bool findTouch = true;
    private LineRenderer currentLine;
    private Touch touch;

    private void Start()
    {
        currentLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount == 0 || !FightMode)
        {
            return;
        }
        if (findTouch)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began && AdjustPointToScreen(8, Input.GetTouch(i).position).x > 0.0f)
                {
                    touch = Input.GetTouch(i);
                    findTouch = false;
                    break;
                }
            }
        }
        if (findTouch)
        {
            return;
        }

        
        switch (touch.phase)
        {
        case TouchPhase.Began:
            AddPoint(currentLine, touch.position);
            break;
        case TouchPhase.Moved:
            if (Vector3.Distance(AdjustPointToScreen(8, touch.position),
                                 currentLine.GetPosition(currentLine.positionCount - 1)) > 0.1f)
            {
                AddPoint(currentLine, touch.position);
            }
            break;
        case TouchPhase.Ended:
            Vector3[] points2 = new Vector3[currentLine.positionCount];
            currentLine.GetPositions(points2);
            PrimitiveContainer[] primitives = HighLevelRecognition.PrimitiveShapeGenerator(points2);
            Debug.Log(SketchOutput.Output(primitives));
            currentLine.positionCount = 0;
            findTouch = true;
            break;
        case TouchPhase.Canceled:
            // ShapeRecognition.Calculate();
            currentLine.positionCount = 0;
            findTouch = true;
            break;
        }
    }

    private void RemoveDuplicates(LineRenderer lineRenderer)
    {
        Vector3 previousPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 2);
        Vector3 currentPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        if (previousPoint[0] == currentPoint[0] && previousPoint[1] == currentPoint[1])
        {
            lineRenderer.positionCount--;
        }
    }

    private void AddPoint(LineRenderer lineRenderer, Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, AdjustPointToScreen(8, position));
    }

    private Vector3 AdjustPointToScreen(float cameraHeight, Vector3 position)
    {
        // Center the point to the screen
        position.x = position.x - (Screen.width / 2f);
        position.y = position.y - (Screen.height / 2f);

        // Adjust the point to the camera
        position.x = (position.x / Screen.width) * cameraHeight * (Screen.width / (float)Screen.height) * 2f;
        position.y = (position.y / Screen.height) * cameraHeight * 2f;

        return position;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Destroy(currentLine);
        }
    }
}
