using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
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
    public Sprite xp0;

    void Start()
    {
        dict = new()
        {
            { 0, xp0 },
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
    }
    /*
    public static Sprite Progressing(int progress)
    {
        if (progress < 10)
            progress++;
        return dict[progress];
    }
    */
    public static Sprite SetProgress(int progress)
    {
        return dict[progress];
    }
}
