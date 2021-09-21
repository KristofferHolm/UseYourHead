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
    private float prevClock = -1f;
    private bool nightTime;
    public Action<bool> NightTime;

    public float ClockPerFrame;

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
        bool newNightTime = nightTime;
        nightTime = (Clock < 6 || Clock > 18);
        if (newNightTime != nightTime)
            NightTime?.Invoke(nightTime);

        Sun.transform.rotation = Quaternion.Euler(Clock * 15, 0, 0) * Quaternion.Euler(SunRotation);
        Moon.transform.rotation = Quaternion.Euler(Clock * 15, 0, 0) * Quaternion.Euler(MoonRotation);
        material.mainTextureOffset = new Vector2((Clock - 12f) / 24, 1);
        ClockPerFrame = Clock - prevClock;
        if (Math.Abs(ClockPerFrame) > 23f)
            ClockPerFrame += 24f;
        prevClock = Clock;
    }
}
