using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour 
{
    [SerializeField] private RectTransform _handleRectTransform;
    [SerializeField] private float _duration = .4f;
    [SerializeField] private Color backgroundActiveColor;
    [SerializeField] private Color handleActiveColor;

    private Toggle toggle;
    private readonly AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Image backgroundImage, handleImage;
    private Color backgroundDefaultColor, handleDefaultColor;
    private Vector2 _handlePosition;
    private float _currentTweeningValue;
    private readonly float _targetTweeningValue = 1;
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        _handlePosition = _handleRectTransform.anchoredPosition;

        backgroundImage = _handleRectTransform.parent.GetComponent<Image>();
        handleImage = _handleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    protected virtual void OnSwitch(bool value)
    {
        StopMoving();
        _moveCoroutine = StartCoroutine(MoveRoutine(
            value ? _handlePosition * -1 : _handlePosition,
            value ? backgroundActiveColor : backgroundDefaultColor,
            value ? handleActiveColor : handleDefaultColor, _duration, _curve));
    }

    private void StopMoving()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private IEnumerator MoveRoutine(Vector2 targetPosition, Color targetBackgroundColor, Color targetHandleColor, float duration, AnimationCurve animationCurve)
    {
        _currentTweeningValue = 0;
        var speed = 1 / duration;
        while (_currentTweeningValue < _targetTweeningValue)
        {
            _currentTweeningValue = Mathf.MoveTowards(_currentTweeningValue, _targetTweeningValue, speed * Time.deltaTime);

            _handleRectTransform.anchoredPosition = Vector2.Lerp(_handleRectTransform.anchoredPosition, targetPosition, animationCurve.Evaluate(_currentTweeningValue));
            backgroundImage.color = Color.Lerp(backgroundImage.color, targetBackgroundColor, speed);
            handleImage.color = Color.Lerp(handleImage.color, targetHandleColor, speed);

            yield return null;
        }

        _handleRectTransform.anchoredPosition = targetPosition;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
