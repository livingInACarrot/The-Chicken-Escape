using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenisBehaviour : MonoBehaviour
{
    private Animator animator;
    private int hours;
    private int minutes;

    // Points to visit on map
    public GameObject home;
    public GameObject barn;
    public GameObject pit;
    public GameObject point1;
    public GameObject point2;
    public GameObject bottomGarden;
    public GameObject hutFence;
    public GameObject hutEntrance;
    public GameObject topRightBed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        hours = TimerClock.Hours();
        minutes = TimerClock.Minutes();

        if (hours == 11 && minutes == 30)
        {

        }
        if (hours == 13 && minutes == 0)
        {

        }
        if (hours == 15 && minutes == 0)
        {

        }
        if (hours == 17 && minutes == 0)
        {

        }
        if (hours == 20 && minutes == 0)
        {

        }
    }
}
