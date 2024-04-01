using UnityEngine;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dayText;
    private int day = 1;
    public static float dayLength = 14f; // ������� �������� ����� ������ ������� ����
    public static float currentTime;

    void Start()
    {     
        currentTime = 11f * 60;   // Start at 11:00 in minutes
        UpdateTimeText();
        UpdateDayText();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * (24 / dayLength);
        // ����� ����������� ������ 15 ������� �����
        if (Mathf.FloorToInt(currentTime % 60) % 15 == 0)
        {
            UpdateTimeText();
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
    public static int Hours()
    {
        return Mathf.FloorToInt(currentTime / 60);
    }
    public static int Minutes()
    {
        return Mathf.FloorToInt(currentTime % 60);
    }
}
