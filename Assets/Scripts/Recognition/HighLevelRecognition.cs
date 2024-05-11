using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighLevelRecognition : MonoBehaviour
{
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
        float[] direction = PreRecognition.DirectionChangeCalculator(points);
        int[] corners = PreRecognition.CornerCalculator(points, PreRecognition.LineLengthCalculator(points),
                                                        PreRecognition.CurvatureCalculator(points, direction));
        int i = 2;
        while (i < corners.Length)
        {
            Vector3[] pointSlice = points[corners[i - 2]..corners[i]];
            float[] directionSlice = PreRecognition.DirectionChangeCalculator(pointSlice);
            float lineDeviation = ShapeRecognition.LineDeviation(pointSlice);
            float[] circle = ShapeRecognition.CircleDeviation(pointSlice);
            float DCR = PreRecognition.DCR_Calculator(directionSlice);
            float NDDE = PreRecognition.NDDE_Calculator(directionSlice, pointSlice);
            if (circle[1] < 0.2f)
            {
                if (DCR > 3f && lineDeviation > 0.1f)
                {
                    // Is Polyline
                    PrimitiveContainer line = new PrimitiveContainer();
                    line.Length = PreRecognition.LineLengthCalculator(pointSlice).Last();
                    line.Rotation = Mathf.Atan2(pointSlice[corners[i - 1] - corners[i - 2]].y - pointSlice[0].y,
                                                pointSlice[corners[i - 1] - corners[i - 2]].x - pointSlice[0].x);
                    line.Type = 0;
                    primitives.Add(line);
                    i += 1;
                }
                else
                {
                    // Is Line
                    PrimitiveContainer line = new PrimitiveContainer();
                    line.Length = PreRecognition.LineLengthCalculator(pointSlice).Last();
                    line.Rotation = Mathf.Atan2(pointSlice[pointSlice.Length - 1].y - pointSlice[0].y,
                                                pointSlice[pointSlice.Length - 1].x - pointSlice[0].x);
                    line.Type = 0;
                    primitives.Add(line);

                    i += 2;
                }
            }
            else
            {
                if (circle[0] < lineDeviation && DCR < 3.5f && NDDE > 0.8f)
                {
                    // Is Arc, Keep going
                    int current = i;
                    for (int u = i + 1; u < corners.Length; u++)
                    {
                        pointSlice = points[corners[i - 2]..corners[u]];
                        directionSlice = PreRecognition.DirectionChangeCalculator(pointSlice);
                        lineDeviation = ShapeRecognition.LineDeviation(pointSlice);
                        circle = ShapeRecognition.CircleDeviation(pointSlice);
                        direction = PreRecognition.DirectionChangeCalculator(pointSlice);
                        DCR = PreRecognition.DCR_Calculator(directionSlice);
                        NDDE = PreRecognition.NDDE_Calculator(directionSlice, pointSlice);

                        if (circle[0] < lineDeviation && circle[1] < 1.2 && DCR < 3.5f && NDDE > 0.85f)
                        {
                            current = u;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    PrimitiveContainer arc = new PrimitiveContainer();
                    arc.Type = 1;
                    arc.Length = PreRecognition.LineLengthCalculator(points[corners[i - 2]..corners[current]]).Last();
                    arc.ConcaveUp = ShapeRecognition.IsConcaveUp(pointSlice, points[corners[i - 1]]);
                    arc.Completeness = circle[1];
                    arc.Rotation = ShapeRecognition.ArcRotation(pointSlice);
                    primitives.Add(arc);
                    i = current + 2;
                }
                else
                {
                    if (DCR > 3f && lineDeviation > 0.2f)
                    {
                        // Is Polyline
                        PrimitiveContainer line = new PrimitiveContainer();
                        line.Length = PreRecognition.LineLengthCalculator(pointSlice).Last();
                        line.Rotation = Mathf.Atan2(pointSlice[corners[i - 1] - corners[i - 2]].y - pointSlice[0].y,
                                                    pointSlice[corners[i - 1] - corners[i - 2]].x - pointSlice[0].x);
                        line.Type = 0;
                        primitives.Add(line);
                        i += 1;
                    }
                    else
                    {
                        // Is Line
                        PrimitiveContainer line = new PrimitiveContainer();
                        line.Length = PreRecognition.LineLengthCalculator(pointSlice).Last();
                        line.Rotation = Mathf.Atan2(pointSlice[pointSlice.Length - 1].y - pointSlice[0].y,
                                                    pointSlice[pointSlice.Length - 1].x - pointSlice[0].x);
                        line.Type = 0;
                        primitives.Add(line);
                        i += 2;
                    }
                }
            }
        }
        if (i == corners.Length)
        {
            Vector3[] pointSlice = points[corners[i - 2]..corners[i - 1]];
            PrimitiveContainer line = new PrimitiveContainer();
            line.Length = PreRecognition.LineLengthCalculator(pointSlice).Last();
            line.Rotation = Mathf.Atan2(pointSlice[pointSlice.Length - 1].y - pointSlice[0].y,
                                        pointSlice[pointSlice.Length - 1].x - pointSlice[0].x);
            line.Type = 0;
            primitives.Add(line);
        }
        float rotationStart = primitives[0].Rotation;

        // Set the first rotation to 0, and the rest to the difference between the first and the current
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
}
