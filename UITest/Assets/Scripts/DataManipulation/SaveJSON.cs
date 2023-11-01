using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveJSON : DataSaveModel
{
    protected override void SaveDataTo(string filePath, List<Data> dataList)
    {
        if (filePath == null) return;

        string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);
        filePath = Path.Combine(filePath, "myData.json");
        File.WriteAllText(filePath, json);
    }

    public static List<Data> LoadFromJSON(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Data>>(json);
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
            return new List<Data>();
        }
    }
}
