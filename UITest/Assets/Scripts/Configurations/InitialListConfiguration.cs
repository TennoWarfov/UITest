using UnityEngine;

[CreateAssetMenu(fileName = "InitialListConfiguration", menuName = "ScriptableObjects/InitialListConfiguration")]
public class InitialListConfiguration : ScriptableObject
{
    public int InitialDataHoldersCount => _initialDataHoldersCount;
    public DataTypePrecursor DataTypePrecursor => _dataTypePrecursor;

    [SerializeField] private int _initialDataHoldersCount;
    [SerializeField] private DataTypePrecursor _dataTypePrecursor;
}
