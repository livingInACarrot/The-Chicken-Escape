using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum DayCycles
{
    Sunrise = 0,
    Day = 1,
    Sunset = 2,
    Night = 3,
}

public class DayNightChange : MonoBehaviour
{
    private Light2D globalLight;
    public Light2D windowLight;

    private DayCycles dayCycle;
    private float cycleCurrentTime = 0;
    private float cycleMaxTime = TimerClock.dayLength * 60 / 24; // 1 hour long trasition
    private int sunr_limit = 7;
    private int day_limit = 19;
    private int suns_limit = 22;
    private int nigh_limit = 4;

    public Color sunrise;
    public Color day;
    public Color sunset;
    public Color night;

    void Start()
    {
        globalLight = GetComponent<Light2D>();
        windowLight.enabled = false;
    }

    void Update()
    {
        dayCycle = WhatCycleIsIt();

        if (TimerClock.Hours() == nigh_limit || TimerClock.Hours() == sunr_limit || 
            TimerClock.Hours() == day_limit || TimerClock.Hours() == suns_limit)
            cycleCurrentTime += Time.deltaTime;
        else
            cycleCurrentTime = 0;
        
        float percent = cycleCurrentTime / cycleMaxTime;

        if (dayCycle == DayCycles.Sunrise)
            globalLight.color = Color.Lerp(sunrise, day, percent);
        else if (dayCycle == DayCycles.Day)
            globalLight.color = Color.Lerp(day, sunset, percent);
        else if (dayCycle == DayCycles.Sunset)
            globalLight.color = Color.Lerp(sunset, night, percent);
        else if (dayCycle == DayCycles.Night)
            globalLight.color = Color.Lerp(night, sunrise, percent);

        if (TimerClock.Hours() == 20)
            windowLight.enabled = true;
        else if (TimerClock.Hours() == 1)
            windowLight.enabled = false;
    }
    DayCycles WhatCycleIsIt()
    {
        int hours = TimerClock.Hours();

        if (hours >= nigh_limit + 1 && hours <= sunr_limit)
            return DayCycles.Sunrise;
        else if (hours >= sunr_limit + 1 && hours <= day_limit)
            return DayCycles.Day;
        else if (hours >= day_limit + 1 && hours <= suns_limit)
            return DayCycles.Sunset;
        else
            return DayCycles.Night;
    }
}

