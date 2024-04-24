using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SketchOutput : MonoBehaviour
{
    public static string Output(PrimitiveContainer[] sketch)
    {
        if (Compare(sketch, shield))
        {
            return "shield";
        }

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

        return "";
    }

    public static bool Compare(PrimitiveContainer[] sketch, PrimitiveContainer[] template, float threshold = 0.4f)
    {
        float sizeCheck = (1.0f / template.Length) * 0.3f;
        int u = 0;
        bool? concaveReverse = null;

        for (int i = 0; i < sketch.Length; i++)
        {
            if (sketch[i].Length < sizeCheck)
            {
                continue;
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
                radianThresholdCheck(sketch[i].Rotation, template[u].Rotation, 1.5f))
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
                    if (thresholdCheck(sketch[i].Completeness, template[u].Completeness, threshold) &&
                        sketch[i].ConcaveUp == template[u].ConcaveUp)
                    {
                        u += 1;
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

    private static bool radianThresholdCheck(float a, float b, float radianThreshold)
    {
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
                                                new PrimitiveContainer(0, 3.90409064f, 0.3343333f, false, 0.0f),
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
}
