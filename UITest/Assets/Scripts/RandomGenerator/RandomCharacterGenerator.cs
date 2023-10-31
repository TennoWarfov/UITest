using UnityEngine;

public class RandomCharacterGenerator
{
    public void GenerateRandomCharacters(string letters, int charactersCount, out string generatedString)
    {
        generatedString = default;
        if (letters == null) return;

        for (int i = 0; i < charactersCount; i++)
        {
            int randomIndex = Random.Range(0, letters.Length);
            generatedString += letters[randomIndex];
        }
    }
    public void GenerateRandomCharacters(string letters, int charactersCount, out int generatedInt)
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
