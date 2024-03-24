using UnityEngine;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dayText;
    private int day = 1;
    public static float dayLength = 14; // Сколько реальных минут длится игровой день
    private float currentTime;
    private bool isTimerRunning = true;

    void Start()
    {     
        currentTime = 11.5f * 60;   // Start at 11:30 in minutes
        UpdateTimeText();
        UpdateDayText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime * (24 / dayLength);
            // Время обновляется каждые 15 игровых минут
            if (Mathf.FloorToInt(currentTime % 60) % 15 == 0)
            {
                UpdateTimeText();
            }
        }
    }

    void UpdateTimeText()
    {
        if (Mathf.FloorToInt(currentTime / 60) >= 24)
        {
            currentTime = 0;
            ++day;
            UpdateDayText();
        }
        int hours = Mathf.FloorToInt(currentTime / 60);
        int minutes = Mathf.FloorToInt(currentTime % 60);

        string timeString = string.Format("{0:D2}:{1:D2}", hours, minutes);
        timeText.text = timeString;
    }
    void UpdateDayText()
    {
        string dayString = "day " + day;
        dayText.text = dayString;
    }
}
