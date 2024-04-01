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
    public static void LowerXP(ref int xp, string tag)
    {
        if (xp > 1)
            --xp;
        SwitchNeeds(xp, tag);
    }
    public static void RaiseXP(ref int xp, string tag)
    {
        if (xp < 10)
            ++xp;
        SwitchNeeds(xp, tag);
    }
    public static void SetXP(int xp, string tag)
    {
        SwitchNeeds(xp, tag);
    }
    public static void SwitchNeeds(int xp, string tag)
    {
        if (tag == "Eat")
            foodImage.sprite = dict[xp];
        else if (tag == "Drink")
            waterImage.sprite = dict[xp];
        else if (tag == "Sleep")
            sleepImage.sprite = dict[xp];
    }

}
