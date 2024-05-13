using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PreRecognition : MonoBehaviour
{
    public static Vector3[] TestCorner(Vector3[] points, float[] curvature)
    {
        List<Vector3> cornerList = new List<Vector3>();
        cornerList.Add(points[0]);
        // Caculates corner range by comparing the distance between points and the line length
        for (int i = 1; i < points.Length; i++)
        {
            if (curvature[i] > 3.5f)
            {
                cornerList.Add(points[i]);
            }
        }

        // Keeps endpoints as corners
        cornerList.Add(points[points.Length - 1]);

        return cornerList.ToArray();
    }

    public static int[] CornerCalculator(Vector3[] points, float[] lineLength, float[] curvature)
    {
        int currentCorner = 0;
        List<float> cornerList = new List<float>();
        cornerList.Add(0.0f);
        float totalCurvature = ExtraMath.Sum(curvature);
        int count = 0;
        // Caculates corner range by comparing the distance between points and the line length
        for (int i = 1; i < points.Length; i++)
        {
            if (Vector3.Distance(points[currentCorner], points[i]) / (lineLength[i] - lineLength[currentCorner]) <=
                0.9f)
            {
                currentCorner = i;
                cornerList.Add(i);
                count += 1;
            }
        }

        // Keeps endpoints as corners
        cornerList.Add(points.Length - 1);
        count += 1;

        // Finds the exact corner using the highest curvature
        List<int> exactCorner = new List<int>();
        exactCorner.Add(0);
        for (int cornerIndex = 1; cornerIndex < count; cornerIndex++)
        {
            int start = (int)cornerList[cornerIndex] -
                        (int)Mathf.Ceil((cornerList[cornerIndex] - cornerList[cornerIndex - 1]) / 1.5f);
            start = Mathf.Max(0, start);

            int end = (int)cornerList[cornerIndex] +
                      (int)Mathf.Ceil((cornerList[cornerIndex + 1] - cornerList[cornerIndex]) / 3f);
            end = Mathf.Min(points.Length, end);
            float max = 0.0f;
            int maxIndex = 0;

            for (int i = start; i < end; i++)
            {
                if (max < curvature[i])
                {
                    max = curvature[i];
                    maxIndex = i;
                }
            }
            exactCorner.Add(maxIndex);
        }
        exactCorner.Add(points.Length - 1);

        return exactCorner.ToArray();
    }

    public static float[] CurvatureCalculator(Vector3[] points, float[] directions)
    {
        float[] curvature = new float[points.Length];
        curvature[0] = 0.0f;
        for (int i = 1; i < points.Length; i++)
        {
            float angle = Mathf.Abs(directions[i] - directions[i - 1]);
            float distance = Vector3.Distance(points[i], points[i - 1]);
            curvature[i] = angle / distance;
        }
        return curvature;
    }

    public static float CornerDeviation(float[] curvature)
    {
        int endTail = (int)Mathf.Ceil(0.9f * curvature.Length);
        int beginingTail = (int)Math.Floor(0.1f * curvature.Length);
        float[] curvatureTrim = new float[endTail - beginingTail];
        for (int i = beginingTail; i < endTail; i++)
        {
            curvatureTrim[i - beginingTail] = curvature[i];
        }

        float curveAverage = ExtraMath.Sum(curvatureTrim) / curvatureTrim.Length;
        float deviationPercent = 0.0f;

        foreach (float curve in curvatureTrim)
        {
            deviationPercent += Mathf.Abs(curve - curveAverage) / curveAverage;
        }

        return deviationPercent / curvatureTrim.Length;
    }

    public static float[] DirectionChangeCalculator(Vector3[] points)
    {
        float[] direction = new float[points.Length];
        direction[0] = 0.0f;
        int[] completeRev = new int[points.Length];

        // Calculates direction
        for (int i = 0; i < points.Length - 1; i++)
        {
            direction[i + 1] = Mathf.Atan2(points[i + 1].y - points[i].y, points[i + 1].x - points[i].x);
        }

        // Calculates the completed rotation
        for (int i = 1; i < direction.Length; i++)
        {
            completeRev[i] = completeRev[i - 1];
            if (Mathf.Abs(direction[i - 1]) >= 2.0f && Mathf.Sign(direction[i - 1]) != Mathf.Sign(direction[i]))
            {
                if (Mathf.Sign(direction[i - 1]) == 1.0f)
                {
                    completeRev[i] += 1;
                }
                else
                {
                    completeRev[i] -= 1;
                }
            }
        }

        // Adds value for each completed rotation
        for (int i = 0; i < direction.Length; i++)
        {
            direction[i] += Mathf.PI * completeRev[i] * 2.0f;
        }
        return direction;
    }

    public static float RadianCalculator(float[] direction)
    {
        // Takes the total rotation and checks if it is over 1.31 radians after removing tails
        int beginingTail = (int)Mathf.Floor(direction.Length * 0.03f);
        int endingTail = (int)Mathf.Floor(direction.Length * 0.97f);

        return Mathf.Abs(direction[endingTail] - direction[beginingTail]) / (Mathf.PI * 2.0f);
    }

    public static bool ClosedFigureCalculator(Vector3[] points, float radians)
    {
        float endDistance = Vector3.Distance(points[0], points[points.Length - 1]);
        float lineLength = 0.0f;
        for (int i = 1; i < points.Length; i++)
        {
            lineLength += Mathf.Abs(Vector3.Distance(points[i], points[i - 1]));
        }
        if (endDistance / lineLength < 0.16f && radians > 0.75f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool OvertracedCalculator(float radians)
    {
        if (radians > 1.25f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float[] LineLengthCalculator(Vector3[] points)
    {
        float[] lineLength = new float[points.Length];
        for (int i = 1; i < points.Length; i++)
        {
            lineLength[i] = lineLength[i - 1] + Mathf.Abs(Vector3.Distance(points[i], points[i - 1]));
        }
        return lineLength;
    }

    public static float DCR_Calculator(float[] direction)
    {
        if (direction.Length < 5)
        {
            return 0.0f;
        }

        int beginingTail = (int)Mathf.Floor(direction.Length * 0.1f);
        int endingTail = (int)Mathf.Floor(direction.Length * 0.9f);
        float maxDC = 0.0f;
        float sumDC = 0.0f;
        for (int i = beginingTail; i < endingTail; i++)
        {
            float directionChange = Mathf.Abs(direction[i + 1] - direction[i]);
            if (directionChange > maxDC)
            {
                maxDC = directionChange;
            }
            sumDC += directionChange;
        }
        float averageDC = sumDC / (endingTail - beginingTail);
        return maxDC / averageDC;
    }

    public static float NDDE_Calculator(float[] direction, Vector3[] points)
    {
        float highestDirection = -10000.0f;
        int highestPoint = 0;
        float lowestDirection = 1000000.0f;
        int lowestPoint = 0;
        float[] lineLength = LineLengthCalculator(points);
        lineLength[0] = 0.0f;
        int completeRev = 0;

        for (int i = 1; i < direction.Length; i++)
        {
            if (direction[i] + Mathf.PI * completeRev > highestDirection)
            {
                highestDirection = direction[i] + Mathf.PI * completeRev;
                highestPoint = i - 1;
            }
            if (direction[i] + Mathf.PI * completeRev < lowestDirection)
            {
                lowestDirection = direction[i] + Mathf.PI * completeRev;
                lowestPoint = i - 1;
            }
        }
        return Mathf.Abs(lineLength[highestPoint] - lineLength[lowestPoint]) / lineLength[lineLength.Length - 1];
    }
}
