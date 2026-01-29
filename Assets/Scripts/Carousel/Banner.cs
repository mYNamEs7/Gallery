using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banner : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup container;
    [SerializeField] private RectTransform canvasRect;

    private void OnEnable()
    {
        UpdateWidth();
    }

    private void OnRectTransformDimensionsChange()
    {
        UpdateWidth();
    }

    private void UpdateWidth()
    {
        float width = canvasRect.rect.width;

        if (Mathf.Approximately(((RectTransform)transform).rect.width, width))
            return;

        ((RectTransform)transform).SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            width
        );

        Canvas.ForceUpdateCanvases();
    }
}
