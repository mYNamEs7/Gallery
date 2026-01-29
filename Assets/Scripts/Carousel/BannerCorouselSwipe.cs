using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BannerCarouselSwipe : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private ScrollRect scrollRect;

    [Header("Settings")]
    [SerializeField] private int bannerCount = 3;
    [SerializeField] private float swipeThreshold = 0.15f;
    [SerializeField] private float snapDuration = 0.25f;
    [SerializeField] private float autoScrollDelay = 5f;

    private int currentIndex;
    private float dragStartPos;
    private Coroutine autoScrollCoroutine;
    private Coroutine snapCoroutine;

    private void OnEnable()
    {
        autoScrollCoroutine = StartCoroutine(AutoScroll());
    }

    private void OnDisable()
    {
        if (autoScrollCoroutine != null)
            StopCoroutine(autoScrollCoroutine);
    }

    private IEnumerator AutoScroll()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoScrollDelay);
            MoveTo(currentIndex + 1);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = scrollRect.horizontalNormalizedPosition;

        if (autoScrollCoroutine != null)
            StopCoroutine(autoScrollCoroutine);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragEndPos = scrollRect.horizontalNormalizedPosition;
        float delta = dragEndPos - dragStartPos;

        if (Mathf.Abs(delta) >= swipeThreshold)
        {
            if (delta < 0)
                MoveTo(currentIndex + 1);
            else
                MoveTo(currentIndex - 1);
        }
        else
            MoveTo(currentIndex);

        autoScrollCoroutine = StartCoroutine(AutoScroll());
    }

    private void MoveTo(int index)
    {
        currentIndex = Mathf.Clamp(index, 0, bannerCount - 1);

        float targetPos = IndexToNormalized(currentIndex);

        if (snapCoroutine != null)
            StopCoroutine(snapCoroutine);

        snapCoroutine = StartCoroutine(SmoothSnap(targetPos));
    }

    private float IndexToNormalized(int index)
    {
        if (bannerCount <= 1) return 0f;
        return (float)index / (bannerCount - 1);
    }

    private IEnumerator SmoothSnap(float target)
    {
        float start = scrollRect.horizontalNormalizedPosition;
        float time = 0f;

        while (time < snapDuration)
        {
            time += Time.deltaTime;
            float t = time / snapDuration;

            scrollRect.horizontalNormalizedPosition =
                Mathf.SmoothStep(start, target, t);

            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }
}
