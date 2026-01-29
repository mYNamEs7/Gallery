using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GalleryController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private GalleryItemView itemPrefab;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;

    [Header("Settings")]
    [SerializeField] private int totalImages = 66;
    [SerializeField] private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    private readonly List<GalleryItemView> items = new();
    
    public event Action<GalleryFilter> OnFilterChanged;
    private GalleryFilter currentFilter;

    private GalleryFilter CurrentFilter
    {
        get => currentFilter;
        set
        {
            if (currentFilter == value)
                return;

            currentFilter = value;
            OnFilterChanged?.Invoke(currentFilter);
        }
    }

    private void Awake()
    {
        scrollRect.onValueChanged.AddListener(_ => CheckVisibleItems());
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        SetupGrid();
    }

    public void SetFilter(GalleryFilter filter)
    {
        CurrentFilter = filter;
        Clear();
        Generate();
        
        Canvas.ForceUpdateCanvases();
        ResetScroll();
        CheckVisibleItems();
    }
    
    private void ResetScroll() => scrollRect.verticalNormalizedPosition = 1f;
    
    private const float BASE_WIDTH = 1440f;
    private const float BASE_GAP = 60f;

    private void SetupGrid()
    {
        bool isTablet = (float)Screen.width / Screen.height > 0.6f;
        int columns = isTablet ? 3 : 2;

        float scale = Screen.width / BASE_WIDTH;
        float gap = BASE_GAP * scale;

        grid.padding.left = Mathf.RoundToInt(gap);
        grid.padding.right = Mathf.RoundToInt(gap);
        grid.spacing = new Vector2(gap, gap);

        float width = content.rect.width;

        float cellSize = (width - gap * (columns + 1)) / columns;

        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraintCount = columns;
    }


    private void Generate()
    {
        int realIndex = 1;
        for (int i = 1; i <= totalImages; i++)
        {
            if (!PassFilter(i)) continue;

            var item = Instantiate(itemPrefab, content);
            item.Init(i, realIndex, baseUrl);
            items.Add(item);
            
            realIndex++;
        }
    }

    private bool PassFilter(int index)
    {
        return CurrentFilter switch
        {
            GalleryFilter.All => true,
            GalleryFilter.Odd => index % 2 == 1,
            GalleryFilter.Even => index % 2 == 0,
            _ => true
        };
    }

    private void Clear()
    {
        foreach (var item in items)
            Destroy(item.gameObject);

        items.Clear();
    }

    private void CheckVisibleItems()
    {
        foreach (var item in items.Where(item => item.IsNearViewport(topPoint, bottomPoint)))
        {
            item.TryLoad();
        }
    }
}
