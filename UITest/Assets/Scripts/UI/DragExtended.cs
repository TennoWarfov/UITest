using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragExtended : Drag, IPointerDownHandler
{
    [SerializeField] private MaskableGraphic[] _graphics;

    private Transform _cachedTransform;
    private Transform _lastParent;
    private Transform _lastHoveredTransfrom;
    private int _lastSiblingIndex;
    private Canvas _canvas;
    private Image _image;
    private GraphicRaycaster _raycaster;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _cachedTransform = transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _lastParent = _cachedTransform.parent;
        _lastSiblingIndex = GetChildIndexInHierarchy(transform);
        Debug.Log(_lastSiblingIndex);
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
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_cachedTransform, mousePosition, null, out localPoint))
        {
            // Выполняем лучевой тест
            PointerEventData pointerEventData = new(EventSystem.current)
            {
                position = eventData.position
            };
            List<RaycastResult> results = new();

            _raycaster.Raycast(pointerEventData, results);

            // Получаем Transform, над которым находится указатель мыши
            if (results.Count > 0)
            {
                Transform hoveredTransform = results[0].gameObject.transform;
                //Debug.Log(hoveredTransform.name);
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

        if (enteringUIElement.TryGetComponent(out SortingPresenter sortingPresenter))
        {
            _cachedTransform.SetParent(sortingPresenter.DataHodlersParent);
        }
        else if (enteringUIElement.TryGetComponent(out DataHolder _))
        {
            _cachedTransform.SetParent(enteringUIElement.parent);
        }
        else
        {
            Debug.Log(_lastParent);
            Debug.Log(_lastSiblingIndex);
            _cachedTransform.SetParent(_lastParent);
            _cachedTransform.SetSiblingIndex(_lastSiblingIndex);
        }

        Debug.Log(eventData.pointerEnter.name);

        _image.raycastTarget = true;
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
