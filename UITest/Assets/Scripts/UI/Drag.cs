using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector, SerializeField] private RectTransform _draggingTarget;

    private bool _isDragging;
    private Vector2 _currentPosition;
    
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_draggingTarget, eventData.position, eventData.pressEventCamera, out _))
            return;

        _isDragging = true;
        _currentPosition = _draggingTarget.anchoredPosition;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_draggingTarget, eventData.position - eventData.delta, eventData.pressEventCamera, out Vector2 oldVector))
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_draggingTarget, eventData.position, eventData.pressEventCamera, out Vector2 newVector))
            return;

        var anchoredPosition = _draggingTarget.anchoredPosition;

        _currentPosition += (Vector2)(_draggingTarget.localRotation * (newVector - oldVector));

        anchoredPosition.x = _currentPosition.x;
        anchoredPosition.y = _currentPosition.y;
        _draggingTarget.anchoredPosition = anchoredPosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    protected virtual void OnValidate()
    {
        if (_draggingTarget == null) _draggingTarget = GetComponent<RectTransform>();
    }
}
