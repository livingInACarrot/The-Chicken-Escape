using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is responsible for needs change
public class NeedsChanging : MonoBehaviour
{
    ChickenInteractions chick;

    public float decreasingSpeedF; // 1 xp decreasing speed (seconds)
    public float recoveringSpeedF; // 1 xp increasing speed (seconds)
    public float decreasingSpeedW;
    public float recoveringSpeedW;
    public float decreasingSpeedS;
    public float recoveringSpeedS;

    private float currentTimeF = 0;
    private float currentTimeW = 0;
    private float currentTimeS = 0;

    public int foodXP = 10;
    public int waterXP = 10;
    public int sleepXP = 10;
    public int eggs = 1;

    void Start()
    {
        chick = GetComponent<ChickenInteractions>();
        decreasingSpeedF = TimerClock.dayLength * 60 / 10 / 2.4f;
        recoveringSpeedF = 2.5f;
        decreasingSpeedW = TimerClock.dayLength * 60 / 10 / 2f;
        recoveringSpeedW = 2.5f;
        decreasingSpeedS = TimerClock.dayLength * 60 / 10 / 1.3f;
        recoveringSpeedS = TimerClock.dayLength * 60 / 10 / 24 * 8;
    }
    void Update()
    {
        UpdateNeeds();
        if (CompareTag("Player"))
            NeedsController.ShowNeeds(this);
    }
    public void UpdateNeeds()
    {
        currentTimeF += Time.deltaTime;
        currentTimeW += Time.deltaTime;
        currentTimeS += Time.deltaTime;
        if (chick.isEating)
        {
            if (currentTimeF > recoveringSpeedF)
            {
                currentTimeF = 0;
                if (foodXP < 10)
                    foodXP++;
            }
        }
        else if (chick.isDrinking)
        {
            if (currentTimeW > recoveringSpeedW)
            {
                currentTimeW = 0;
                if (waterXP < 10)
                    waterXP++;
            }
        }
        else if (chick.isSleeping)
        {
            if (currentTimeS > recoveringSpeedS)
            {
                currentTimeS = 0;
                if (sleepXP < 10)
                    sleepXP++;
            }
        }
        else
        {
            if (currentTimeF > decreasingSpeedF)
            {
                currentTimeF = 0;
                if (foodXP > 1)
                    foodXP--;
            }
            if (currentTimeW > decreasingSpeedW)
            {
                currentTimeW = 0;
                if (waterXP > 1)
                    waterXP--;
            }
            if (currentTimeS > decreasingSpeedS)
            {
                currentTimeS = 0;
                if (sleepXP > 1)
                    sleepXP--;
            }
        }
    }
}
