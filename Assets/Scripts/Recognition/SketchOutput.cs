using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SketchOutput : MonoBehaviour
{
    public static string Output(PrimitiveContainer[] sketch)
    {
        string filePath = Application.dataPath + "/Templates/Lightning.json";
        string fileContents = File.ReadAllText(filePath);
        PrimitiveContainer[] template = JsonConvert.DeserializeObject<PrimitiveContainer[]>(fileContents);
        if (Compare(sketch, template))
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                gameObject.GetComponent<EnemyHealth>().TakeDamage(20.0f);
            }
            return "Lightning";
        }

        filePath = Application.dataPath + "/Templates/Shield.json";
        fileContents = File.ReadAllText(filePath);
        template = JsonConvert.DeserializeObject<PrimitiveContainer[]>(fileContents);
        if (Compare(sketch, template))
        {
            PlayerHealth.Instance.SetInvincability(1.5f);
            return "Shield";
        }

        filePath = Application.dataPath + "/Templates/Water.json";
        fileContents = File.ReadAllText(filePath);
        template = JsonConvert.DeserializeObject<PrimitiveContainer[]>(fileContents);
        if (Compare(sketch, template))
        {
            return "Water";
        }

        return "None";
    }

    public static bool Compare(PrimitiveContainer[] sketch, PrimitiveContainer[] template, float threshold = 0.25f)
    {
        float sizeCheck = (1.0f / template.Length) * 0.2f;
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
}
