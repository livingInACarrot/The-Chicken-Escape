using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is responsible for needs change
public class NeedsChanging : MonoBehaviour
{
    private Image needImage;
    private int correntXP;
    public float secondsPerXP = 5;     // 1 xp decreasing speed (seconds)
    public float recoveringSpeed = 3;  // 1 xp increasing speed (seconds)
    private float currentTime = 0;

    void Start()
    {
        needImage = GetComponent<Image>();
        correntXP = 10;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (NeedsController.isEating && CompareTag("Eat"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                needImage.sprite = NeedsController.RaiseXP(ref correntXP);
            }
        }
        else if (NeedsController.isDrinking && CompareTag("Drink"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                needImage.sprite = NeedsController.RaiseXP(ref correntXP);
            }
        }
        else if (NeedsController.isSleeping && CompareTag("Sleep"))
        {
            if (currentTime > recoveringSpeed)
            {
                currentTime = 0;
                needImage.sprite = NeedsController.RaiseXP(ref correntXP);
            }
        }
        else
        {
            if (currentTime > secondsPerXP)
            {
                currentTime = 0;
                needImage.sprite = NeedsController.LowerXP(ref correntXP);
            }
        }

    }
}
