using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Resizer : MonoBehaviour
{
    private enum Side
    {
        Width, Height
    }

    private enum AspectRatio
    {
        OneToOne = 1, TwoToOne = 2, Custom = 3
    }

    [SerializeField] private Side _resizeSide = Side.Width;
    [SerializeField] private AspectRatio _aspectRatio = AspectRatio.OneToOne;
    [SerializeField] private float _aspectRatioValue = 1f;
    
    private Image _image;
    private RectTransform _rectTransform;
    private Vector2 _startSize;
    private float _scaleValue;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _scaleValue = _aspectRatio == AspectRatio.Custom ? _aspectRatioValue : (int)_aspectRatio;           
    }

    private void Update()
    {
        _startSize = _rectTransform.rect.size;

        if (_resizeSide == Side.Width)
        {
            _image.rectTransform.sizeDelta = new Vector2(Mathf.RoundToInt(_startSize.y * _scaleValue), 0);
        }
        else
        {
            _image.rectTransform.sizeDelta = new Vector2(0, Mathf.RoundToInt(_startSize.x * _scaleValue));
        }
    }
}
