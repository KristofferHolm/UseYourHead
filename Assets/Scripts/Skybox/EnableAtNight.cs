using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAtNight : MonoBehaviour
{
    public float DelayOn = 2f;
    public float DelayOff = 2f;
    public bool EnableAtDay = false;
    void Start()
    {
        DayNightCycle.Instance.HoursBeforeNight += Activate;
        if (DayNightCycle.Instance.IsHoursBeforeNight)
            gameObject.SetActive(!EnableAtDay);
    }

    private void Activate(bool on)
    {
        var newOn = EnableAtDay ? !on : on;
        CityManager.Instance.CoroutineDelayActivation(newOn ? DelayOn : DelayOff,()=> gameObject.SetActive(newOn));
    }
}
