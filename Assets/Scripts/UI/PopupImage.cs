using UnityEngine;
using UnityEngine.UI;

public class PopupImage : PopupBase
{
    public static PopupImage Instance;

    [SerializeField] private Image image;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public void Show(Sprite sprite)
    {
        image.sprite = sprite;
        base.Show();
    }
}