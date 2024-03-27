using UnityEngine;
using TMPro;

public enum DayCycles
{
    Sunrise = 0,
    Day = 1,
    Sunset = 2,
    Night = 3,
    Midnight = 4
}

public class DayNightChange : MonoBehaviour
{
    // ��� ����� ������� ���
    public TMP_Text timeText;

    [Header("Controllers")]

    [Tooltip("Global light 2D component, we need to use this object to place light in all map objects")]
    public Light globalLight; // global light

    [Tooltip("This is a current cycle time, you can change for private float but we keep public only for debug")]
    public float cycleCurrentTime = 0; // current cycle time

    [Tooltip("This is a cycle max time in seconds, if current time reach this value we change the state of the day and night cyles")]
    public float cycleMaxTime = 60; // duration of cycle

    [Tooltip("Enum with multiple day cycles to change over time, you can add more types and modify whatever you want to fits on your project")]
    public DayCycles dayCycle = DayCycles.Sunrise; // default cycle

    [Header("Cycle Colors")]

    [Tooltip("Sunrise color, you can adjust based on best color for this cycle")]
    public Color sunrise; // Eg: 6:00 at 10:00

    [Tooltip("(Mid) Day color, you can adjust based on best color for this cycle")]
    public Color day; // Eg: 10:00 at 19:00

    [Tooltip("Sunset color, you can adjust based on best color for this cycle")]
    public Color sunset; // Eg: 19:00 at 21:00

    [Tooltip("Night color, you can adjust based on best color for this cycle")]
    public Color night; // Eg: 21:00 at 00:00

    [Tooltip("Midnight color, you can adjust based on best color for this cycle")]
    public Color midnight; // Eg: 00:00 at 06:00

    [Header("Objects")]
    [Tooltip("Objects to turn on and off based on day night cycles, you can use this example for create some custom stuffs")]
    public Light[] mapLights; // enable/disable in day/night states

    void Start()
    {
        dayCycle = WhatCycleIsIt();  // ���������� ����� ��� �������
        globalLight.color = day;    // start global color at day
    }

    void Update()
    {
        // Update cycle time
        cycleCurrentTime += Time.deltaTime;

        // Check if cycle time reach cycle duration time
        if (cycleCurrentTime >= cycleMaxTime)
        {
            cycleCurrentTime = 0; // back to 0 (restarting cycle time)
            dayCycle = WhatCycleIsIt();
        }
        
        // If reach final state we back to sunrise (Enum id 0)
        //if (dayCycle > DayCycles.Midnight)
        //    dayCycle = 0;

        // percent it's an value between current and max time to make a color lerp smooth
        float percent = cycleCurrentTime / cycleMaxTime;

        // Sunrise state (you can do a lot of stuff based on every cycle state, like enable animals only in sunrise )
        if (dayCycle == DayCycles.Sunrise)
        {
            ControlLightMaps(false); // disable map light (keep enable only at night)
            globalLight.color = Color.Lerp(sunrise, day, percent);
        }

        // Mid Day state
        if (dayCycle == DayCycles.Day)
            globalLight.color = Color.Lerp(day, sunset, percent);

        // Sunset state
        if (dayCycle == DayCycles.Sunset)
            globalLight.color = Color.Lerp(sunset, night, percent);

        // Night state
        if (dayCycle == DayCycles.Night)
        {
            ControlLightMaps(true); // enable map lights (disable only in day states)
            globalLight.color = Color.Lerp(night, midnight, percent);
        }

        // Midnight state
        if (dayCycle == DayCycles.Midnight)
            globalLight.color = Color.Lerp(midnight, day, percent);
    }

    void ControlLightMaps(bool status)
    {
        // loop in light array of objects to enable/disable
        if (mapLights.Length > 0)
            foreach (Light _light in mapLights)
                _light.gameObject.SetActive(status);
    }

    DayCycles WhatCycleIsIt()
    {
        var time_arr = timeText.text.Split(":");
        int hours = int.Parse(time_arr[0]);
        if (hours > 5 && hours <= 10)
            return DayCycles.Sunrise;
        else if(hours > 10 && hours <= 19)
            return DayCycles.Day;
        else if (hours > 19 && hours <= 21)
            return DayCycles.Sunset;
        else if (hours > 21 && hours <= 23)
            return DayCycles.Midnight;
        else
            return DayCycles.Night;
    }
}
