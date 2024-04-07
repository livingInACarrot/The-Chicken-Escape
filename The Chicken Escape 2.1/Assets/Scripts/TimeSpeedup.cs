using UnityEngine;
using UnityEngine.EventSystems;

public class TimeSpeedup : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static float speedup = 1f;
    private float speedupValue = 3f;  // x3

    public void OnPointerDown(PointerEventData eventData)
    {
        speedup = speedupValue;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        speedup = 1;
    }
}
