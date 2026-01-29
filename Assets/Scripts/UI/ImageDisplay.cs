using UnityEngine;

[ExecuteAlways]
public class ImageDisplay : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float width = rect.rect.width;
        rect.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            width
        );
    }
}