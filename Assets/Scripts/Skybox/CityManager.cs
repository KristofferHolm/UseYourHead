using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public static CityManager Instance;

    public Material LightOn1, LightOn2, LightOff;
    
    private void Awake()
    {
        Instance = this;
    }
    public void CoroutineDelayActivation(float clockHours, Action callback)
    {
        StartCoroutine(DelayActivation(clockHours, callback));
    }
    public IEnumerator DelayActivation(float clockHours, Action callback)
    {
        while (clockHours > 0)
        {
            clockHours -= DayNightCycle.Instance.ClockPerFrame;
            yield return null;
        }
        callback.Invoke();
    }
}
