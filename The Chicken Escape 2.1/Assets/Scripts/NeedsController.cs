using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class changes needs pictures and current xp
public static class NeedsController
{
    public static bool isEating;
    public static bool isDrinking;
    public static bool isSleeping;
    public static Dictionary<int, Sprite> dict;
    public static Sprite LowerXP(ref int xp)
    {
        if (xp > 1)
        {
            --xp;
        }
        return dict[xp];
    }
    public static Sprite RaiseXP(ref int xp)
    {
        if (xp < 10)
        {
            ++xp;
        }
        return dict[xp];
    }
}
