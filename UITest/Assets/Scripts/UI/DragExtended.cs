using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragExtended : Drag
{
    [SerializeField] private MaskableGraphic[] _graphics;

    private Transform _cachedTransform;
    private Transform _lastParent;
    private Transform _lastHoveredTransfrom;
    private int _lastSiblingIndex;
    private Canvas _canvas;
    private Image _image;
    private GraphicRaycaster _raycaster;
    private DataHolder _dataHolder;
    private SortingPresenter _lastSortingPresenter;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _dataHolder = GetComponent<DataHolder>();
        _lastSortingPresenter = GetComponentInParent<SortingPresenter>();
        _cachedTransform = transform;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _lastSortingPresenter.UnregisterDataHodler(_dataHolder);
        _lastParent = _cachedTransform.parent;
        _lastSiblingIndex = GetChildIndexInHierarchy(transform);
        _image.raycastTarget = false;
        _cachedTransform.SetParent(_canvas.transform);

        base.OnBeginDrag(eventData);

        for (int i = 0; i < _graphics.Length; i++)
        {
            _graphics[i].maskable = false;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        Vector2 mousePosition = Input.mousePosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_cachedTransform, mousePosition, null, out _))
        {
            PointerEventData pointerEventData = new(EventSystem.current)
            {
                position = eventData.position
            };
            List<RaycastResult> results = new();

            _raycaster.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                _lastHoveredTransfrom = results[0].gameObject.transform;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        for (int i = 0; i < _graphics.Length; i++)
        {
            _graphics[i].maskable = true;
        }

        var enteringUIElement = eventData.pointerEnter.transform;

        if (enteringUIElement.TryGetComponent(out IDraggableContainer draggableContainer))
        {
            _cachedTransform.SetParent(draggableContainer.DraggablesContainer);
            if (_lastHoveredTransfrom.TryGetComponent(out Drag _))
            {
                int siblingIndex = GetChildIndexInHierarchy(_lastHoveredTransfrom);
                SetRectSibling(siblingIndex);
            }
            else
            {
                int totalSiblings = draggableContainer.DraggablesContainer.childCount;
                int lastSiblingIndex = totalSiblings - 1;
                SetRectSibling(lastSiblingIndex);
            }
        }
        else if (enteringUIElement.TryGetComponent(out Drag _))
        {
            _cachedTransform.SetParent(enteringUIElement.parent);
            int enteringUiIndex = GetChildIndexInHierarchy(enteringUIElement);
            int totalSiblings = enteringUIElement.parent.childCount;
            int lastSiblingIndex = totalSiblings - 1;

            if(enteringUiIndex == 0)
                SetRectSibling(0);
            else if(enteringUiIndex == lastSiblingIndex)
                SetRectSibling(lastSiblingIndex);
            else
                SetRectSibling(enteringUiIndex);
        }
        else
        {
            Reset();
        }

        _image.raycastTarget = true;
    }

    private void Reset()
    {
        _cachedTransform.SetParent(_lastParent);
        _cachedTransform.SetSiblingIndex(_lastSiblingIndex);
        Register(_lastSiblingIndex);
    }

    private void SetRectSibling(int index)
    {
        _cachedTransform.SetSiblingIndex(index);
        Register(index);
    }

    private void Register(int index)
    {
        _lastSortingPresenter = _cachedTransform.GetComponentInParent<SortingPresenter>();
        _lastSortingPresenter.RegisterDataHolder(_dataHolder, index);
    }

    private int GetChildIndexInHierarchy(Transform child)
    {
        int index = 0;
        for (int i = 0; i < child.parent.childCount; i++)
        {
            if (child.parent.GetChild(i) == child)
            {
                index = i;
                break;
            }
        }

        return index;
    }
}
