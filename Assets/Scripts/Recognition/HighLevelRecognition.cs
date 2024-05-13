using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighLevelRecognition : MonoBehaviour
{
    static Vector3[] pointSlice;
    static float[] directionSlice;
    static float lineDeviation;
    static float[] circle;
    static float DCR;
    static float NDDE;
    static int[] corners;
    static float[] direction;
    
    public static float RadianConvert(float radian)
    {
        radian = radian % (2 * Mathf.PI);
        if (radian < 0)
        {
            radian += 2 * Mathf.PI;
        }
        return radian;
    }

    public static PrimitiveContainer[] PrimitiveShapeGenerator(Vector3[] points)
    {
        if (points.Length <= 6)
        {
            return new PrimitiveContainer[] {};
        }
        List<PrimitiveContainer> primitives = new List<PrimitiveContainer>();
        direction = PreRecognition.DirectionChangeCalculator(points);
        corners = PreRecognition.CornerCalculator(points, PreRecognition.LineLengthCalculator(points),
                                                        PreRecognition.CurvatureCalculator(points, direction));
        int i = 2;
        while (i < corners.Length)
        {
            CalculateIndicators(i-2, i, points);
            if (circle[0] < lineDeviation && DCR < 3.0f && NDDE > 0.8f)
            {
                // Is Arc, Keep going
                int current = i;
                for (int u = i + 1; u < corners.Length; u++)
                {
                    CalculateIndicators(i-2, u, points);
                    if (circle[0] < lineDeviation && circle[1] < 1.2 && DCR < 3.0f && NDDE > 0.85f)
                    {
                        current = u;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (circle[1] > 0.25f)
                {
                    primitives.Add(ArcCreator(i, current, points));
                }
                else if (DCR > 3.5f || lineDeviation > 0.2f)
                {
                    for (int u = i - 2; u < Mathf.Max(current, i); u++)
                    {
                        primitives.Add(LineCreator(u, i - 2, points));
                    }
                }
                else
                {
                    primitives.Add(LineCreator(i-2, i, points));
                }
                i = current + 2;
            }
            else if (DCR > 3.5f || lineDeviation > 0.2f)
            {
                // Is Polyline
                primitives.Add(LineCreator(i-1, i-2, points));
                i += 1;
            }
            else
            {
                // Is Line
                primitives.Add(LineCreator(i, i-2, points));
                i += 2;
            }
        }
        if (i == corners.Length)
        {
            primitives.Add(LineCreator(i-1, i-2, points));
        }
        
        // Set the first rotation to 0, and the rest to the difference between the first and the current
        float rotationStart = primitives[0].Rotation;
        primitives[0].Rotation = 0.0f;
        for (int a = 1; a < primitives.Count; a++)
        {
            primitives[a].Rotation = RadianConvert(primitives[a].Rotation - rotationStart);
        }

        // Normalize the length of the primitives
        float totalLength = 0.0f;
        foreach (PrimitiveContainer p in primitives)
        {
            totalLength += p.Length;
        }

        for (int a = 0; a < primitives.Count; a++)
        {
            primitives[a].Length = primitives[a].Length / totalLength;
        }
        return primitives.ToArray();
    }

    private static PrimitiveContainer ArcCreator(int index, int current, Vector3[] points)
    {
        PrimitiveContainer arc = new PrimitiveContainer();
        arc.Type = 1;
        arc.Length = PreRecognition.LineLengthCalculator(points[corners[index - 2]..corners[current]]).Last();
        arc.ConcaveUp = ShapeRecognition.IsConcaveUp(pointSlice, points[corners[index - 1]]);
        arc.Completeness = circle[1];
        arc.Rotation = ShapeRecognition.ArcRotation(pointSlice);
        return arc;
    }

    private static PrimitiveContainer LineCreator(int end, int begin, Vector3[] points)
    {
        PrimitiveContainer line = new PrimitiveContainer();
        line.Length = Vector3.Distance(points[corners[end]], points[corners[begin]]);
        line.Rotation = Mathf.Atan2(points[corners[end]].y - points[corners[begin]].y,
                                    points[corners[end]].x - points[corners[begin]].x);
        line.Type = 0;
        return line;
    }

    private static void CalculateIndicators(int begin, int end, Vector3[] points)
    {
        pointSlice = points[corners[begin]..corners[end]];
        directionSlice = PreRecognition.DirectionChangeCalculator(pointSlice);
        lineDeviation = ShapeRecognition.LineDeviation(pointSlice);
        circle = ShapeRecognition.CircleDeviation(pointSlice);
        direction = PreRecognition.DirectionChangeCalculator(pointSlice);
        DCR = PreRecognition.DCR_Calculator(directionSlice);
        NDDE = PreRecognition.NDDE_Calculator(directionSlice, pointSlice);
    }
}
