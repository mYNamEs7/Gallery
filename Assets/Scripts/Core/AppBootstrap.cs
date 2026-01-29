using System;
using System.Collections;
using UnityEngine;

public class AppBootstrap : MonoBehaviour
{
    [SerializeField] private GalleryController gallery;
    [SerializeField] private GalleryFilter firstTab;

    public GalleryFilter FirstTab => firstTab;
    public static AppBootstrap Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        gallery.SetFilter(firstTab);
    }
}
