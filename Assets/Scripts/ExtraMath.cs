using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraMath : MonoBehaviour
{
    public static float Sum(float[] values)
    {
        float sum = 0;
        foreach (float value in values)
        {
            sum += value;
        }
        return sum;
    }

    public static float[] PerpendicularBisect(Vector3 begin, Vector3 end)
    {
        float slope = (end.y - begin.y) / (end.x - begin.x) + 0.00000000001f;
        slope = -1 / slope;
        Vector3 centerPoint = new Vector3((begin.x + end.x) / 2, ((begin.y + end.y) / 2), 0);
        float yIntercept = centerPoint.y - (centerPoint.x * slope);

        // RecognizerDebuger.Instance.DrawLine(new float[] {slope, yIntercept});
        return new float[] { slope, yIntercept };
    }

    public static Vector2 LineIntercept(float[] line1, float[] line2)
    {
        float x = (line2[1] - line1[1]) / (line1[0] - line2[0] + 0.0000001f);
        float y = line1[0] * x + line1[1];

        return new Vector3(x, y, 0);
    }

    public static void LinearRegression(Vector3[] points, out float rSquared, out float yIntercept, out float slope)
    {
        float sumOfX = 0;
        float sumOfY = 0;
        float sumOfXSq = 0;
        float sumOfYSq = 0;
        float sumCodeviates = 0;

        for (var i = 0; i < points.Length; i++)
        {
            var x = points[i].x;
            var y = points[i].y;
            sumCodeviates += x * y;
            sumOfX += x;
            sumOfY += y;
            sumOfXSq += x * x;
            sumOfYSq += y * y;
        }

        var count = points.Length;
        var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
        var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

        var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
        var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
        var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

        var meanX = sumOfX / count;
        var meanY = sumOfY / count;
        var dblR = rNumerator / Mathf.Sqrt(rDenom);

        rSquared = dblR * dblR;
        yIntercept = meanY - ((sCo / ssX) * meanX);
        slope = sCo / ssX;
    }
}
