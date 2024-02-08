using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SketchJsonStorage : MonoBehaviour
{

    public static SketchJsonStorage Instance = null;
    public string fileName;
    public List<Vector3[]> sketchList = new List<Vector3[]>();

    public void Awake()
    {
        Instance = this;
    }

    public void AddJSON(Vector3[] sketch)
    {
        string filePath = Application.dataPath + "/" + fileName + ".json";

        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            sketchList = JsonConvert.DeserializeObject<List<Vector3[]>>(fileContents);
        }

        sketchList.Add(sketch);

        string json = JsonConvert.SerializeObject(
            sketchList, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        File.WriteAllText(filePath, json);
    }
}
