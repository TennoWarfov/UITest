using System;

public interface ISelectable
{
    public event Action<ISelectable> OnSelected;
    public void Deselect();
}
