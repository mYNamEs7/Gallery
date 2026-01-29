using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotButton : MonoBehaviour
{
    [SerializeField] private BannerCorousel bannerCorousel;
    [SerializeField] private List<int> indexes;
    [SerializeField] private GameObject activeObject;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => bannerCorousel.MoveTo(indexes[0]));
    }

    private void Start()
    {
        OnIndexChanged(1);
    }

    private void OnEnable()
    {
        BannerCorousel.OnIndexChanged += OnIndexChanged;
    }

    private void OnDisable()
    {
        BannerCorousel.OnIndexChanged -= OnIndexChanged;
    }

    private void OnIndexChanged(int targetIndex)
    {
        activeObject.SetActive(indexes.Contains(targetIndex));
    }
}
