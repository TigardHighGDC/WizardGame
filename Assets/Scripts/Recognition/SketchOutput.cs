using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SketchOutput : MonoBehaviour
{
    public static string Output(PrimitiveContainer[] sketch)
    {
        if (Compare(sketch, water))
        {
            return "water";
        }

        if (Compare(sketch, fire))
        {
            return "fire";
        }

        if (Compare(sketch, lightning))
        {
            return "lightning";
        }

        if (Compare(sketch, earth))
        {
            return "earth";
        }

        if (Compare(sketch, shield))
        {
            return "shield";
        }

        return "";
    }

    public static bool Compare(PrimitiveContainer[] sketch, PrimitiveContainer[] template, float threshold = 0.65f)
    {
        float sizeCheck = (1.0f / sketch.Length) * 0.3f;
        int u = 0;
        bool? concaveReverse = null;
        float prevRotation = -10f;
        Debug.Log("Primitives");
        foreach (PrimitiveContainer p in sketch)
        {
            Debug.Log(p.Length);
        }

        for (int i = 0; i < sketch.Length; i++)
        {
            // Removes small lines
            if (sketch[i].Length < sizeCheck)
            {
                continue;
            }

            if (prevRotation == -10.0f)
            {
                prevRotation = sketch[i].Rotation;
            }
            else
            {
                prevRotation = sketch[i-1].Rotation;
            }

            if (template.Length == u)
            {
                return false;
            }
            if (sketch[i].Type != template[u].Type)
            {
                return false;
            }
            if (thresholdCheck(sketch[i].Length, template[u].Length, threshold) &&
                radianThresholdCheck(sketch, template, i, u, 0.7f, prevRotation))
            {
                if (sketch[i].Type == 1)
                {
                    if (concaveReverse == null)
                    {
                        concaveReverse = sketch[i].ConcaveUp != template[u].ConcaveUp;
                    }
                    if (concaveReverse == true)
                    {
                        sketch[i].ConcaveUp = !sketch[i].ConcaveUp;
                    }
                    if (completenessCheck(sketch[i].Completeness, template[u].Completeness, 0.2f) &&
                        sketch[i].ConcaveUp == template[u].ConcaveUp)
                    {
                        u += 1;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    u += 1;
                }
            }
            else
            {
                return false;
            }
        }
        return template.Length == u;
    }

    private static bool thresholdCheck(float a, float b, float threshold)
    {
        return a * (1 - threshold) < b && a * (1 + threshold) > b;
    }

    private static bool completenessCheck(float a, float b, float threshold)
    {
        return a - threshold < b && a + threshold > b;
    }

    private static bool radianThresholdCheck(PrimitiveContainer[] sketch, PrimitiveContainer[] template, int i, int u,
                                             float radianThreshold, float prevRotation)
    {
        float a = sketch[i].Rotation;
        float b = template[u].Rotation;

        a -= prevRotation;
        if (u > 0)
        {
            b -= template[u - 1].Rotation;
        }

        // Removes negative values
        a = (a + (Mathf.PI * 4)) % (Mathf.PI * 2);
        b = (b + (Mathf.PI * 4)) % (Mathf.PI * 2);

        if (b + radianThreshold > 2 * Mathf.PI)
        {
            if (a < b + radianThreshold - (2 * Mathf.PI))
            {
                return true;
            }
        }
        if (b - radianThreshold < 0)
        {
            if (b - radianThreshold + (2 * Mathf.PI) < a)
            {
                return true;
            }
        }
        if (b - radianThreshold < a && a < b + radianThreshold)
        {
            return true;
        }

        return false;
    }

    // Spell Raw Data
    public static PrimitiveContainer[] fire = { new PrimitiveContainer(0, 0.0f, 0.457411557f, false, 0.0f),
                                                new PrimitiveContainer(0, 3.5f, 0.3343333f, false, 0.0f),
                                                new PrimitiveContainer(1, 1.798421f, 0.208255142f, true,
                                                                       0.408551544f) };
    public static PrimitiveContainer[] lightning = { new PrimitiveContainer(0, 0.0f, 0.4076188f, false, 0.0f),
                                                     new PrimitiveContainer(0, 2.40771317f, 0.368411481f, false, 0.0f),
                                                     new PrimitiveContainer(0, 6.26135445f, 0.223969668f, false,
                                                                            0.0f) };
    public static PrimitiveContainer[] shield = { new PrimitiveContainer(1, 0.0f, 1.0f, false, 1.0f) };
    public static PrimitiveContainer[] water = { new PrimitiveContainer(1, 0.0f, 0.5047563f, true, 0.8546553f),
                                                 new PrimitiveContainer(1, 0.5974314f, 0.495243728f, true,
                                                                        0.505029261f) };
    public static PrimitiveContainer[] earth = { new PrimitiveContainer(0, 0.0f, 0.41f, false, 0.0f),
                                                 new PrimitiveContainer(0, 4.697068f, 0.39f, false, 0.0f),
                                                 new PrimitiveContainer(0, 3.5f, 0.2f, false, 0.0f) };
}
