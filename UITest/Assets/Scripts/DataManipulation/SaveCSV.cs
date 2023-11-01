using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveCSV : DataSaveModel
{
    protected override void SaveDataTo(string filePath, List<Data> dataList)
    {
        if (filePath == null) return;

        StringBuilder sb = new();
        sb.AppendLine("Number,Name");

        foreach (var data in dataList)
        {
            sb.AppendLine(data.Int + "," + data.String);
        }

        filePath = Path.Combine(filePath, "myData.csv");
        File.WriteAllText(filePath, sb.ToString());
    }

    public static List<Data> LoadFromCSV(string filePath)
    {
        List<Data> dataList = new();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values.Length == 2)
                {
                    if (int.TryParse(values[0], out int number))
                    {
                        dataList.Add(new Data(number, values[1]));
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }

        return dataList;
    }
}
