using System;
using System.Collections;
using UnityEngine;

public class BannerCorousel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform container;
    [SerializeField] private SwipeInput swipeInput;

    [Header("Settings")]
    [SerializeField] private int bannerCount = 3;
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float autoScrollDelay = 5f;

    private int currentIndex;
    private float bannerWidth;
    private bool isAnimating;
    private Coroutine moveCoroutine;
    private Coroutine autoScrollCoroutine;
    public static Action<int> OnIndexChanged;

    private void Start()
    {
        bannerWidth = ((RectTransform)transform).rect.width;

        swipeInput.OnSwipeLeft += NextSwipe;
        swipeInput.OnSwipeRight += PreviousSwipe;

        currentIndex = 1;
        UpdatePositionInstant();

        autoScrollCoroutine = StartCoroutine(AutoScroll());
    }

    private void OnDestroy()
    {
        swipeInput.OnSwipeLeft -= NextSwipe;
        swipeInput.OnSwipeRight -= PreviousSwipe;
    }

    private IEnumerator AutoScroll()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoScrollDelay);
            NextSwipe();
        }
    }
    
    private void ResetAutoScroll()
    {
        if (autoScrollCoroutine != null)
            StopCoroutine(autoScrollCoroutine);

        autoScrollCoroutine = StartCoroutine(AutoScroll());
    }

    private void NextSwipe()
    {
        MoveTo(currentIndex + 1);
        ResetAutoScroll();
    }

    private void PreviousSwipe()
    {
        MoveTo(currentIndex - 1);
        ResetAutoScroll();
    }
    
    public void MoveTo(int targetIndex)
    {
        if (isAnimating)
            return;

        OnIndexChanged?.Invoke(targetIndex);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        currentIndex = targetIndex;
        moveCoroutine = StartCoroutine(AnimateTo(currentIndex));
    }

    private IEnumerator AnimateTo(int index)
    {
        isAnimating = true;

        float startX = container.anchoredPosition.x;
        float targetX = -index * bannerWidth;
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float t = time / animationDuration;
            container.anchoredPosition = new Vector2(
                Mathf.Lerp(startX, targetX, Mathf.SmoothStep(0, 1, t)), 0);
            yield return null;
        }

        container.anchoredPosition = new Vector2(targetX, 0);

        // -------- LOOP FIX --------
        if (index == 0) // клон последнего
            currentIndex = bannerCount;
        else if (index == bannerCount + 1) // клон первого
            currentIndex = 1;

        UpdatePositionInstant();

        isAnimating = false;
    }

    private void UpdatePositionInstant()
    {
        container.anchoredPosition = new Vector2(-currentIndex * bannerWidth, 0);
    }
}
