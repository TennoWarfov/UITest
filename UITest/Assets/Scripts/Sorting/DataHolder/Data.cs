using System;

[Serializable]
public class Data
{
    public int Int;
    public string String;

    public Data(int @int, string @string)
    {
        Int = @int;
        String = @string;
    }
}
