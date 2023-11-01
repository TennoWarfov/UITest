using AnotherFileBrowser.Windows;
using System.Collections.Generic;
using System.IO;

public abstract class DataSaveModel 
{
    public virtual void SaveData(List<Data> dataList)
    {
        BrowserProperties browserProperties = new("Select saving file path:")
        {
            restoreDirectory = true
        };

        new FileBrowser().OpenFolderBrowser(browserProperties, filePath => { SaveDataTo(filePath, dataList); });
    }

    protected abstract void SaveDataTo(string filePath, List<Data> dataList);
}
