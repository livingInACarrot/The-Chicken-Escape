using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class initialises static NeedsController class
public class NeedsInitialiser : MonoBehaviour
{
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
        NeedsController.isEating = false;
        NeedsController.isDrinking = false;
        NeedsController.isSleeping = false;
        NeedsController.dict = new()
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
    }
}
