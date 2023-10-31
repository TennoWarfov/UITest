using TMPro;
using UnityEngine;

public class DataHolder: MonoBehaviour, IComparable
{
    public int Int
    {
        get => _number;
        set
        {
            _number = value;
            _numberText.text = _number.ToString();
        }
    }

    public string String 
    { 
        get => _string;
        set
        {
            _string = value;
            _stringText.text = _string; 
        }
    }

    public Transform Transform => _cachedTransform;

    [SerializeField] private TextMeshProUGUI _numberText; 
    [SerializeField] private TextMeshProUGUI _stringText;
    [HideInInspector, SerializeField] private Transform _cachedTransform;

    private int _number;
    private string _string;
    private readonly string _intLetters = "0123456789";
    private readonly string _stringLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private void Start()
    {
        RandomCharacterGenerator randomCharacterGenerator = new();
        randomCharacterGenerator.GenerateRandomCharacters(_intLetters, 3, out int num);
        randomCharacterGenerator.GenerateRandomCharacters(_stringLetters, 10, out string str);

        Int = num;
        String = str;
    }

    private void OnValidate()
    {
        if (_cachedTransform == null) _cachedTransform = transform;
    }
}
