using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class changes needs pictures and current xp
public class NeedsController : MonoBehaviour
{
    public Image foodImageInit;
    public Image waterImageInit;
    public Image sleepImageInit;
    public static Image foodImage;
    public static Image waterImage;
    public static Image sleepImage;

    public static Dictionary<int, Sprite> dict;
    public Sprite xp10;
    public Sprite xp9;
    public Sprite xp8;
    public Sprite xp7;
    public Sprite xp6;
    public Sprite xp5;
    public Sprite xp4;
    public Sprite xp3;
    public Sprite xp2;
    public Sprite xp1;

    void Start()
    {
        dict = new()
        {
            { 1, xp1 },
            { 2, xp2 },
            { 3, xp3 },
            { 4, xp4 },
            { 5, xp5 },
            { 6, xp6 },
            { 7, xp7 },
            { 8, xp8 },
            { 9, xp9 },
            { 10, xp10 }
        };
        foodImage = foodImageInit;
        waterImage = waterImageInit;
        sleepImage = sleepImageInit;
    }
    public static void ShowNeeds(NeedsChanging chick)
    {
        foodImage.sprite = dict[chick.foodXP];
        waterImage.sprite = dict[chick.waterXP];
        sleepImage.sprite = dict[chick.sleepXP];
    }
    /*
    public static void UpdateNeeds(ChickenInteractions chick)
    {
        currentTimeF += Time.deltaTime;
        currentTimeW += Time.deltaTime;
        currentTimeS += Time.deltaTime;
        if (chick.isEating)
        {
            if (currentTimeF > recoveringSpeedF)
            {
                currentTimeF = 0;
                if (chick.foodXP < 10)
                    chick.foodXP++;
            }
        }
        else if (chick.isDrinking)
        {
            if (currentTimeW > recoveringSpeedW)
            {
                currentTimeW = 0;
                if (chick.waterXP < 10)
                    chick.waterXP++;
            }
        }
        else if (chick.isSleeping)
        {
            if (currentTimeS > recoveringSpeedS)
            {
                currentTimeS = 0;
                if (chick.sleepXP < 10)
                    chick.sleepXP++;
            }
        }
        else
        {
            if (currentTimeF > decreasingSpeedF)
            {
                currentTimeF = 0;
                if (chick.foodXP > 1)
                    chick.foodXP--;
            }
            if (currentTimeW > decreasingSpeedW)
            {
                currentTimeW = 0;
                if (chick.waterXP > 1)
                    chick.waterXP--;
            }
            if (currentTimeS > decreasingSpeedS)
            {
                currentTimeS = 0;
                if (chick.sleepXP > 1)
                    chick.sleepXP--;
            }
        }
    }
    */
}
