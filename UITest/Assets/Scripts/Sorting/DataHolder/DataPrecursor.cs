using System;

[Serializable]
public class DataPrecursor
{
    public string Data;
    public int DataLength;

    public DataPrecursor(string data, int dataLength)
    {
        Data = data;
        DataLength = dataLength;
    }
}
