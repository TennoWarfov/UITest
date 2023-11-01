using TMPro;
using UnityEngine;

public class DataHolder: MonoBehaviour, IComparable, IProduct, IGenerable
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

    public void Initialize() { }

    private void OnValidate()
    {
        if (_cachedTransform == null) _cachedTransform = transform;
    }
}
