using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAtNight : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        DayNightCycle.Instance.NightTime += Activate;
        if (DayNightCycle.Instance.IsNight)
            meshRenderer.enabled = false;
    }

    private void Activate(bool on)
    {
        StartCoroutine(DelayActivation(UnityEngine.Random.Range(0, 3), () => meshRenderer.enabled = on));
    }
    IEnumerator DelayActivation(float clockHours, Action callback)
    {
        while (clockHours > 0)
        {
            clockHours -= DayNightCycle.Instance.ClockPerFrame;
            yield return null;
        }
        callback.Invoke();
    }
}
