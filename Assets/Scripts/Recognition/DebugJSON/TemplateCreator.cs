using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TemplateCreator : MonoBehaviour
{
    public static TemplateCreator Instance = null;
    public string fileName;
    public List<Vector3[]> sketchList = new List<Vector3[]>();

    public void Awake()
    {
        Instance = this;
    }

    public void AddJSON(PrimitiveContainer[] sketch)
    {
        string filePath = Application.dataPath + "/" + fileName + ".json";

        string json = JsonConvert.SerializeObject(
            sketch, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                         NullValueHandling = NullValueHandling.Ignore });
        File.WriteAllText(filePath, json);
    }
}
