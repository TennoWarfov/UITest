using System;
using UnityEngine;

[Serializable]
public class DataTypePrecursor
{
    public DataPrecursor IntDataPrecursor => _intDataPrecursor;
    public DataPrecursor StringDataPrecursor => _stringDataPrecursor;

    [SerializeField] private DataPrecursor _intDataPrecursor = new("0123456789", 3);
    [SerializeField] private DataPrecursor _stringDataPrecursor = new("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", 10);
}
