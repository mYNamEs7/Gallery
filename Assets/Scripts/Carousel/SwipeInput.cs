using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Settings")]
    [SerializeField] private float minSwipeDistance = 80f;

    private Vector2 startPos;
    private bool swipeInProgress;

    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
        swipeInProgress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!swipeInProgress)
            return;

        DetectSwipe(eventData.position);
        swipeInProgress = false;
    }

    void DetectSwipe(Vector2 endPos)
    {
        Vector2 delta = endPos - startPos;

        // игнорируем вертикальные свайпы
        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            return;

        if (Mathf.Abs(delta.x) < minSwipeDistance)
            return;

        if (delta.x < 0)
            OnSwipeLeft?.Invoke();
        else
            OnSwipeRight?.Invoke();
    }
}