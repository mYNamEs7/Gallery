using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SubscribeController
{
    public enum Plan
    {
        Week,
        Month,
        Year,
    }

    private static Plan currentPlan;

    public static Action<Plan> OnPlanChanged;

    public static void SetPlan(Plan plan)
    {
        currentPlan = plan;
        OnPlanChanged?.Invoke(plan);
    }
}
