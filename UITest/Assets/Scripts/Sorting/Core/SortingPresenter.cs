using EventBusPattern;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortingPresenter : MonoBehaviour, IEventReceiver<IntSortEvent>, IEventReceiver<StringSortEvent>
{
    public UniqueId Id { get; } = new();
    public Transform DataHodlersParent => _dataHoldersParent;

    [SerializeField] private TextMeshProUGUI _itemsCount;
    [SerializeField] private SortingEventBusHolder _eventBusHolder;
    [SerializeField] private Transform _dataHoldersParent;
    [SerializeField] private List<DataHolder> _dataHolders = new();

    private IntAscendingComparer _intAscendingComparer;
    private IntDescendingComparer _intDescendingComparer;
    private StringAscendingComparer _stringAscendingComparer;
    private StringDescendingComparer _stringDescendingComparer;

    private void Start()
    {
        if(_dataHolders.Count == 0)
        {
            DataHolder[] dataHolders = GetComponentsInChildren<DataHolder>();
            _dataHolders = new();
            _dataHolders.AddRange(dataHolders);
        }

        UpdateItemsCount();

        _intAscendingComparer = new();
        _intDescendingComparer = new();
        _stringAscendingComparer = new();
        _stringDescendingComparer = new();

        _eventBusHolder.EventBus.Register(this as IEventReceiver<IntSortEvent>);
        _eventBusHolder.EventBus.Register(this as IEventReceiver<StringSortEvent>);
    }

    private void OnDestroy()
    {
        _eventBusHolder.EventBus.Unregister(this as IEventReceiver<IntSortEvent>);
        _eventBusHolder.EventBus.Unregister(this as IEventReceiver<StringSortEvent>);
    }

    public void OnEvent(IntSortEvent @event)
    {
        if (@event.Value)
            Sort(_intAscendingComparer);
        else
            Sort(_intDescendingComparer);
    }

    public void OnEvent(StringSortEvent @event)
    {
        if (@event.Value)
            Sort(_stringAscendingComparer);
        else
            Sort(_stringDescendingComparer);
    }

    private void Sort(IComparer<IComparable> comparer)
    {
        _dataHolders.Sort(comparer);

        for (int i = 0; i < _dataHolders.Count; i++)
        {
            _dataHolders[i].Transform.SetSiblingIndex(i);
        }
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
