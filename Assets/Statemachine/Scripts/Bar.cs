using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public RectTransform foreground;
    public RectTransform background;

    [SerializeField]
    private float _maxWidth;

    void OnEnable()
    {
        _maxWidth = background.rect.width;
    }

    public void SetBar(float currentValue, float maxValue)
    {
        float percentage = currentValue / maxValue;
        foreground.sizeDelta = new Vector2(percentage * _maxWidth, foreground.sizeDelta.y);
    }
}
