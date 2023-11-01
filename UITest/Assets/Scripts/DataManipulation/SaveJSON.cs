using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveJSON : MonoBehaviour
{
    public static void SaveToJSON(List<DataHolder> dataList, string filePath)
    {
        string json = JsonConvert.SerializeObject(dataList);
        File.WriteAllText(filePath, json);
    }

    public static List<DataHolder> LoadFromJSON(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<DataHolder>>(json);
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
            return new List<DataHolder>();
        }
    }
}
