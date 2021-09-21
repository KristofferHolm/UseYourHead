using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance;
    public enum TimeFormatenum
    {
        RealTime,
        FakeTime,
        SetTime,
    }
    public TimeFormatenum TimeFormat;
    public float TimelapseSpeed;
    [Range(0f,24f)]
    public float Clock;
    public Material material;
    public GameObject Moon, Sun;
    public Vector3 SunRotation;
    public Vector3 MoonRotation;
    public Action<bool> NightTime, HoursBeforeNight, DawnTime;
    public float ClockPerFrame;
    public bool IsNight
    {
        get
        {
            return (Clock < 6 || Clock > 18);
        }
    }
    public bool IsHoursBeforeNight
    {
        get
        {
            return (Clock < 3 || Clock > 15);
        }
    }
    public bool IsDawn
    {
        get
        {
            return (Clock > 4 && Clock < 8);
        }
    }


    private float prevClock = -1f;
    private bool nightTime;
    private bool hoursBeforeNight;
    private bool dawnTime;
    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        Update();
    }
    public void Update()
    {
        switch (TimeFormat)
        {
            case TimeFormatenum.RealTime:
                float hour = DateTime.Now.Hour;
                float min = DateTime.Now.Minute;
                float second = DateTime.Now.Second;
                Clock = hour + min / 60f + second / 6000f;
                break;
            case TimeFormatenum.FakeTime:
                Clock = prevClock + TimelapseSpeed * Time.deltaTime;
                if (Clock > 24)
                    Clock -= 24;
                break;
            case TimeFormatenum.SetTime:
                break;
        }

        if (prevClock != -1 && prevClock == Clock)
        {
            ClockPerFrame = 0f;
            return;
        }

        CheckTimes();

        Sun.transform.rotation = Quaternion.Euler(SunRotation) * Quaternion.Euler(Clock * 15, 0, 0);
        Moon.transform.rotation = Quaternion.Euler(MoonRotation) * Quaternion.Euler(Clock * 15, 0, 0);
        material.mainTextureOffset = new Vector2((Clock - 12f) / 24, 1);
        ClockPerFrame = Clock - prevClock;
        if (Math.Abs(ClockPerFrame) > 23f)
            ClockPerFrame += 24f;
        prevClock = Clock;
    }

    private void CheckTimes()
    {
        bool newHoursBeforeNight = hoursBeforeNight;
        hoursBeforeNight = IsHoursBeforeNight;
        if (newHoursBeforeNight != hoursBeforeNight)
            HoursBeforeNight?.Invoke(hoursBeforeNight);

        bool newNightTime = nightTime;
        nightTime = IsNight;
        if (newNightTime != nightTime)
            NightTime?.Invoke(nightTime);

        bool newDawnTime = dawnTime;
        dawnTime = IsDawn;
        if (newDawnTime != dawnTime)
            DawnTime?.Invoke(dawnTime);
    }
}
