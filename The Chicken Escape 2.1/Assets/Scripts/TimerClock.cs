using UnityEngine;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public TMP_Text timeText;
    private float timerDuration = 7 * 60; // 7 minutes
    private float currentTime;
    private bool isTimerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 11.5f * 60; // Start at 11:30 in minutes
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 20 * 60) // If it's 20:00
            {
                isTimerRunning = false;
                currentTime = 11.5f * 60; // Reset to 11:30
            }

            UpdateTimeText();
        }
    }

    void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(currentTime / 60);
        int minutes = Mathf.FloorToInt(currentTime % 60);

        string timeString = string.Format("{0:D2}:{1:D2}", hours, minutes);
        timeText.text = timeString;
    }
}
