using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribeButton : MonoBehaviour
{
    [SerializeField] private SubscribeController.Plan plan;
    [SerializeField] private GameObject activeObject;
    
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SubscribeController.SetPlan(plan));
    }

    private void Start()
    {
        SubscribeController.SetPlan(SubscribeController.Plan.Week);
    }

    private void OnEnable()
    {
        SubscribeController.OnPlanChanged += OnPlanChanged;
    }

    private void OnPlanChanged(SubscribeController.Plan targetPlan)
    {
        activeObject.SetActive(plan == targetPlan);
    }
}
