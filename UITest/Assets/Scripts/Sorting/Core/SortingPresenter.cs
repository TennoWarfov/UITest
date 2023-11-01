using EventBusPattern;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortingPresenter : MonoBehaviour, IEventReceiver<IntSortEvent>, IEventReceiver<StringSortEvent>, IEventReceiver<DataLoadEvent>, IDraggableContainer
{
    public UniqueId Id { get; } = new();
    public Transform DraggablesContainer => _dataHoldersParent;
    public List<Data> Datas => GetDataFrom(_dataHolders);

    [SerializeField] private TextMeshProUGUI _itemsCount;
    [SerializeField] private SortingEventBusHolder _eventBusHolder;
    [SerializeField] private Transform _dataHoldersParent;
    [SerializeField] private DataHolderFactory _dataHoldersFactory;
    [SerializeField] private InitialListConfiguration _initialListConfig;
    [SerializeField] private List<DataHolder> _dataHolders = new();

    private RandomDataGenerator _randomDataGenerator;
    private IntAscendingComparer _intAscendingComparer;
    private IntDescendingComparer _intDescendingComparer;
    private StringAscendingComparer _stringAscendingComparer;
    private StringDescendingComparer _stringDescendingComparer;

    private bool _intSortingEnabled;
    private bool _stringSortingEnabled;

    private void Start()
    {
        DataHoldersInitialization();

        UpdateItemsCount();

        _intAscendingComparer = new();
        _intDescendingComparer = new();
        _stringAscendingComparer = new();
        _stringDescendingComparer = new();

        _eventBusHolder.EventBus.Register(this as IEventReceiver<IntSortEvent>);
        _eventBusHolder.EventBus.Register(this as IEventReceiver<StringSortEvent>);
        _eventBusHolder.EventBus.Register(this as IEventReceiver<DataLoadEvent>);
    }

    private void OnDestroy()
    {
        _eventBusHolder.EventBus.Unregister(this as IEventReceiver<IntSortEvent>);
        _eventBusHolder.EventBus.Unregister(this as IEventReceiver<StringSortEvent>);
        _eventBusHolder.EventBus.Unregister(this as IEventReceiver<DataLoadEvent>);
    }

    private void DataHoldersInitialization()
    {
        _randomDataGenerator = new();
        RandomDataPrecursor randomDataPrecursor = new(_initialListConfig.DataTypePrecursor);

        for (int i = 0; i < _initialListConfig.InitialDataHoldersCount; i++)
        {
            DataHolder dataHolder = (DataHolder)_dataHoldersFactory.GetProduct(_dataHoldersParent);
            _randomDataGenerator.GenerateData(randomDataPrecursor, dataHolder);
            _dataHolders.Add(dataHolder);
        }
    }

    #region Event processing
    public void OnEvent(IntSortEvent @event)
    {
        _intSortingEnabled = @event.Value;

        if (@event.Value)
            Sort(_intAscendingComparer);
        else
            Sort(_intDescendingComparer);

        UpdateItemsCount();
    }

    public void OnEvent(StringSortEvent @event)
    {
        _stringSortingEnabled = @event.Value;

        if (@event.Value)
            Sort(_stringAscendingComparer);
        else
            Sort(_stringDescendingComparer);

        UpdateItemsCount();
    }

    public void OnEvent(DataLoadEvent @event)
    {
        ClearData();

        GenerateData(@event.Datas);

        SortIfSortingEnabled();
    }
    #endregion

    private void Sort(IComparer<IComparable> comparer)
    {
        _dataHolders.Sort(comparer);

        for (int i = 0; i < _dataHolders.Count; i++)
        {
            _dataHolders[i].Transform.SetSiblingIndex(i);
        }
    }

    private void SortIfSortingEnabled()
    {
        if (_intSortingEnabled)
            Sort(_intAscendingComparer);
        else
            Sort(_stringAscendingComparer);

        UpdateItemsCount();
    }

    public void RegisterDataHolder(DataHolder dataHolder, int index)
    {
        if (!_dataHolders.Contains(dataHolder))
            _dataHolders.Insert(index, dataHolder);

        SortIfSortingEnabled();
    }

    public void UnregisterDataHolder(DataHolder dataHolder)
    {
        if(_dataHolders.Contains(dataHolder))
            _dataHolders.Remove(dataHolder);

        SortIfSortingEnabled();
    }

    private void GenerateData(List<Data> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            DataHolder dataHolder = (DataHolder)_dataHoldersFactory.GetProduct(_dataHoldersParent);

            dataHolder.Int = datas[i].Int;
            dataHolder.String = datas[i].String;

            _dataHolders.Add(dataHolder);
        }
    }

    private void ClearData()
    {
        for (int i = 0; i < _dataHolders.Count; i++)
        {
            Destroy(_dataHolders[i].gameObject);
        }

        _dataHolders.Clear();
    }

    private List<Data> GetDataFrom(List<DataHolder> dataHolders)
    {
        List<Data> datas = new();
        for (int i = 0; i < dataHolders.Count; i++)
        {
            datas.Add(new Data(dataHolders[i].Int, dataHolders[i].String));
        }

        return datas;
    }

    private void UpdateItemsCount()
    {
        _itemsCount.text = $"Items count: {_dataHolders.Count}";
    }

#if UNITY_EDITOR
    [ContextMenu("Get all data holders children")]
    private void GetAllDataHoldersChildren()
    {
        DataHolder[] dataHolders = GetComponentsInChildren<DataHolder>();
        _dataHolders = new();
        _dataHolders.AddRange(dataHolders);
    }
#endif
}
