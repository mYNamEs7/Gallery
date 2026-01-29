using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    [SerializeField] private GalleryController gallery;
    [SerializeField] private GalleryFilter filter;
    [SerializeField] private Button button;
    
    [Header("Visual")]
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private Color originalFontColor;
    [SerializeField] private Color selectedFontColor;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            gallery.SetFilter(filter);
        });
    }

    private void Start()
    {
        SetActive(filter == AppBootstrap.Instance.FirstTab);
    }

    private void OnEnable()
    {
        gallery.OnFilterChanged += OnFilterChanged;
    }

    private void OnDisable()
    {
        gallery.OnFilterChanged -= OnFilterChanged;
    }

    private void OnFilterChanged(GalleryFilter newFilter) => SetActive(newFilter == filter);

    private void SetActive(bool isActive)
    {
        if (isActive)
        {
            buttonText.color = selectedFontColor;
            selectedObject.SetActive(true);
        }
        else
        {
            buttonText.color = originalFontColor;
            selectedObject.SetActive(false);
        }
    }
}