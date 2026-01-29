using UnityEngine;
using UnityEngine.UI;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField] protected Button closeButton;

    protected virtual void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);

        Hide();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Hide();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}