using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class SortingEventSender : MonoBehaviour, ISelectable
{
    public event Action<ISelectable> OnSelected;

    [SerializeField] private TogglesSelectionProvider _selectionProvider;
    [SerializeField] protected SortingEventBusHolder _eventBusHolder;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(SendSortingEvent);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(SendSortingEvent);
    }

    private void OnEnable()
    {
        _selectionProvider.RegisterSelectable(this);
    }

    private void OnDisable()
    {
        _selectionProvider.UnregisterSelectable(this);
    }

    protected virtual void SendSortingEvent(bool arg0)
    {
        if(arg0)
            OnSelected.Invoke(this);
    }

    private void OnValidate()
    {
        if (_toggle == null) _toggle = GetComponent<Toggle>();
    }

    public void Deselect()
    {
        _toggle.isOn = false;
    }
}
