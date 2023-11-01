using UnityEngine.UI;
using UnityEngine;
using EventBusPattern;
using System.Collections.Generic;

public class DataManipulationPresenter : MonoBehaviour
{
    [SerializeField] private SortingEventBusHolder _sortingEventBusHolder;
    [SerializeField] private Button _jsonSaveButton;
    [SerializeField] private Button _csvSaveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private SortingPresenter _sortingPresenter;

    private SaveJSON _saveJSON;
    private SaveCSV _saveCSV;
    private DataLoadModel _loadModel;

    private void Awake()
    {
        _saveJSON = new();
        _saveCSV = new();
        _loadModel = new();

        _jsonSaveButton.onClick.AddListener(SaveDataToJSON);
        _csvSaveButton.onClick.AddListener(SaveDataToCSV);
        _loadButton.onClick.AddListener(LoadData);
    }

    private void OnDestroy()
    {
        _jsonSaveButton.onClick.RemoveListener(SaveDataToJSON);
        _csvSaveButton.onClick.RemoveListener(SaveDataToCSV);
        _loadButton.onClick.RemoveListener(LoadData);
    }

    private void SaveDataToJSON()
    {
#if UNITY_STANDALONE_WIN
        _saveJSON.SaveData(_sortingPresenter.Datas);
#endif
    }

    private void SaveDataToCSV()
    {
#if UNITY_STANDALONE_WIN
        _saveCSV.SaveData(_sortingPresenter.Datas);
#endif
    }
    
    private void LoadData()
    {
#if UNITY_STANDALONE_WIN
        List<Data> loadedData = _loadModel.LoadData();

        if (loadedData == null) return;

        _sortingEventBusHolder.EventBus.Raise(new DataLoadEvent(loadedData));
#endif
    }
}
