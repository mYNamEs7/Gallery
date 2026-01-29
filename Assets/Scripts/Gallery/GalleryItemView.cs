using UnityEngine;
using UnityEngine.UI;

public class GalleryItemView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject premiumBadge;
    [SerializeField] private Button button;

    private int index;
    private string url;
    private bool loaded;

    public void Init(int index, int realIndex, string baseUrl)
    {
        this.index = realIndex;
        url = $"{baseUrl}{index}.jpg";

        bool isPremium = this.index % 4 == 0;
        premiumBadge.SetActive(isPremium);

        button.onClick.AddListener(() =>
        {
            if (!image.sprite) return;
            
            if (isPremium)
                PopupPremium.Instance.Show(image.sprite);
            else
                PopupImage.Instance.Show(image.sprite);
        });
    }

    public bool IsNearViewport(Transform top, Transform bottom)
    {
        // Проверка, что элемент находится рядом с видимой зоной
        Vector3[] corners = new Vector3[4];
        ((RectTransform)transform).GetWorldCorners(corners);
        
        return corners[0].y < top.transform.position.y && corners[1].y > bottom.transform.position.y;
    }

    public void TryLoad()
    {
        if (loaded) return;

        loaded = true;
        StartCoroutine(ImageLoader.Load(url, image));
    }
}