using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRecognition : MonoBehaviour
{
    public static float LineDeviation(Vector3[] points)
    {
        float rSquared;
        float yIntercept;
        float slope;

        ExtraMath.LinearRegression(points, out rSquared, out yIntercept, out slope);
        float totalDeviation = 0f;

        // Calculates deviation from best fit line
        for (int i = 0; i < points.Length; i++)
        {
            float y = yIntercept + slope * points[i].x;
            totalDeviation += Mathf.Abs(points[i].y - y);
        }

        return totalDeviation / points.Length;
    }

    public static float[] CircleDeviation(Vector3[] points)
    {

        float lineLength = 0.0f;

        for (int i = 1; i < points.Length; i++)
        {
            lineLength += Mathf.Abs(Vector3.Distance(points[i], points[i - 1]));
        }

        Vector3 centerPoint = FindCenterpoint(points);
        // Find the average radius
        float radius = 0.0f;

        for (int i = 0; i < points.Length; i++)
        {
            radius += Vector3.Distance(points[i], centerPoint);
        }
        radius /= points.Length;

        float deviationTotal = 0.0f;

        foreach (Vector3 point in points)
        {
            deviationTotal += Mathf.Abs(Vector3.Distance(point, centerPoint) - radius) / radius;
        }

        return new float[] { deviationTotal / points.Length, lineLength / (radius * 2 * Mathf.PI) };
    }

    public static Vector3 FindCenterpoint(Vector3[] points)
    {
        // Perpendicular bisectors intersect at the center of the circle
        int pq = (int)(points.Length / 3);
        float[] line1 = ExtraMath.PerpendicularBisect(points[0], points[pq]);
        float[] line2 = ExtraMath.PerpendicularBisect(points[pq], points[pq * 2]);
        float[] line3 = ExtraMath.PerpendicularBisect(points[pq * 2], points[points.Length - 1]);

        Vector3 center1 = ExtraMath.LineIntercept(line1, line2);
        Vector3 center2 = ExtraMath.LineIntercept(line1, line3);
        Vector3 center3 = ExtraMath.LineIntercept(line2, line3);

        Vector3 centerPoint;

        float distance1 = Vector3.Distance(center1, center2);
        float distance2 = Vector3.Distance(center2, center3);
        float distance3 = Vector3.Distance(center1, center3);

        // Find the two closest points and average them to find the center
        if (distance1 <= distance2 && distance1 <= distance3)
        {
            centerPoint = new Vector3((center1.x + center2.x) / 2, (center1.y + center2.y) / 2, 0);
        }
        else if (distance2 <= distance1 && distance2 <= distance3)
        {
            centerPoint = new Vector3((center2.x + center3.x) / 2, (center2.y + center3.y) / 2, 0);
        }
        else
        {
            centerPoint = new Vector3((center1.x + center3.x) / 2, (center1.y + center3.y) / 2, 0);
        }
        return centerPoint;
    }

    public static bool IsConcaveUp(Vector3[] points, Vector3 corner)
    {
        Vector3 centerPoint = FindCenterpoint(points);

        float startToCenter = Mathf.Atan2(centerPoint.y - points[0].y, centerPoint.x - points[0].x);
        float centerToCorner = Mathf.Atan2(corner.y - centerPoint.y, corner.x - centerPoint.x);

        centerToCorner = centerToCorner - startToCenter;
        if (centerToCorner < 0)
        {
            centerToCorner += 2 * Mathf.PI;
        }
        if (centerToCorner < Mathf.PI)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static float ArcRotation(Vector3[] points)
    {
        Vector3 centerPoint = FindCenterpoint(points);
        return Mathf.Atan2(points[points.Length - 1].y - centerPoint.y, points[points.Length - 1].x - centerPoint.x);
    }
}
