using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is responsible for needs change
public class NeedsChanging : MonoBehaviour
{
    //private Image needImage;
    //private int currentXP = 10;
    public float secondsPerXP = 5;     // 1 xp decreasing speed (seconds)
    public float recoveringSpeed = 3;  // 1 xp increasing speed (seconds)
    private float currentTime = 0;

    void Start()
    {
        //needImage = GetComponent<Image>();
        //NeedsController.SetXP(currentXP);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (ChickenInteractions.isEating && CompareTag("Eat"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                NeedsController.RaiseXP(ref ChickenInteractions.foodXP, "Eat");
            }
        }
        else if (ChickenInteractions.isDrinking && CompareTag("Drink"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                NeedsController.RaiseXP(ref ChickenInteractions.waterXP, "Drink");
            }
        }
        else if (ChickenInteractions.isSleeping && CompareTag("Sleep"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                NeedsController.RaiseXP(ref ChickenInteractions.sleepXP, "Sleep");
            }
        }
        else
        {
            if (currentTime > secondsPerXP)
            {
                currentTime = 0;
                NeedsController.LowerXP(ref ChickenInteractions.foodXP, "Eat");
                NeedsController.LowerXP(ref ChickenInteractions.waterXP, "Drink");
                NeedsController.LowerXP(ref ChickenInteractions.sleepXP, "Sleep");
            }
        }

    }
}
