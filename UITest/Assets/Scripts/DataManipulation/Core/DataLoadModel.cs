using AnotherFileBrowser.Windows;
using System.Collections.Generic;
using System.IO;

public class DataLoadModel
{
    public virtual List<Data> LoadData()
    {
        var bp = new BrowserProperties
        {
            filter = "Text files (*.json, *.csv) | *.json; *.csv",
            filterIndex = 0
        };

        List<Data> dataList = new();
        new FileBrowser().OpenFileBrowser(bp, filePath =>
        {
            LoadDataFrom(filePath, out dataList);
        });

        return dataList;
    }

    private void LoadDataFrom(string filePath, out List<Data> dataList)
    {
        dataList = new();

        if (filePath == null) return;

        if (Path.GetExtension(filePath) == ".json")
            dataList = SaveJSON.LoadFromJSON(filePath);
        else
            dataList = SaveCSV.LoadFromCSV(filePath);
    }
}
