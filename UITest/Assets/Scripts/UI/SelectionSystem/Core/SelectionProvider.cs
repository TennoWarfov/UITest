using System.Collections.Generic;
using UnityEngine;

public abstract class SelectionProvider : MonoBehaviour
{
    [SerializeField] private List<ISelectable> _selectables = new();

    public virtual void RegisterSelectable(ISelectable selectable)
    {
        _selectables.Add(selectable);
        selectable.OnSelected += SelectableSelected;
    }

    public virtual void UnregisterSelectable(ISelectable selectable)
    {
        selectable.OnSelected -= SelectableSelected;
        _selectables.Remove(selectable);
    }

    protected virtual void SelectableSelected(ISelectable selectable)
    {
        for (int i = 0; i < _selectables.Count; i++)
        {
            if (_selectables[i] == selectable)
                continue;

            _selectables[i].Deselect();
        }
    }
}
