using UnityEngine;
using UnityEngine.UI;

public class PopupPremium : PopupBase
{
    public static PopupPremium Instance;

    private Sprite spriteToShowAfterHide;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public void Show(Sprite sprite)
    {
        spriteToShowAfterHide = sprite;
        base.Show();
    }

    public override void Hide()
    {
        if (spriteToShowAfterHide && PopupImage.Instance)
        {
            PopupImage.Instance.Show(spriteToShowAfterHide);
            spriteToShowAfterHide = null;
        }

        base.Hide();
    }
}