using Unity.VisualScripting;
using UnityEngine;
public readonly struct RandomDataPrecursor
{
    public readonly string IntData;
    public readonly int IntDataLength;
    public readonly string StringData;
    public readonly int StringDataLength;

    public RandomDataPrecursor(DataTypePrecursor dataTypePrecursor)
    {
        IntData = dataTypePrecursor.IntDataPrecursor.Data;
        IntDataLength = dataTypePrecursor.IntDataPrecursor.DataLength;
        StringData = dataTypePrecursor.StringDataPrecursor.Data;
        StringDataLength = dataTypePrecursor.StringDataPrecursor.DataLength;
    }
}

public class RandomDataGenerator
{
    public void GenerateData(RandomDataPrecursor randomDataPrecursor, IGenerable generable)
    {
        GenerateInt(randomDataPrecursor.IntData, randomDataPrecursor.IntDataLength, out int generatedInt);
        GenerateString(randomDataPrecursor.StringData, randomDataPrecursor.StringDataLength, out string generatedString);
        generable.Int = generatedInt;
        generable.String = generatedString;
    }

    private void GenerateString(string letters, int charactersCount, out string generatedString)
    {
        generatedString = default;
        if (letters == null) return;

        for (int i = 0; i < charactersCount; i++)
        {
            int randomIndex = Random.Range(0, letters.Length);
            generatedString += letters[randomIndex];
        }
    }

    private void GenerateInt(string letters, int charactersCount, out int generatedInt)
    {
        generatedInt = default;
        string generatedString = default;
        if (letters == null) return;

        for (int i = 0; i < charactersCount; i++)
        {
            int randomIndex = Random.Range(0, letters.Length);
            generatedString += letters[randomIndex];
        }

        int.TryParse(generatedString, out generatedInt);
    }
}
