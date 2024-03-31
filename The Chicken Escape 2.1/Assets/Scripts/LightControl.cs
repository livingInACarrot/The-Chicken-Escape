using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightControl : MonoBehaviour
{
    public float duration = 5;    //TimerClock.dayLength * 10;
    [SerializeField] private Gradient gradient;
    private Light2D _light;
    private float _startTime;

    private void Start()
    {
        _light = GetComponent<Light2D>();
        _startTime = Time.time;
    }

    void Update()
    {
        var elapsed = Time.time - _startTime;
        var pers = Mathf.Sin(elapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
        pers = Mathf.Clamp01(pers);
        _light.color = gradient.Evaluate(pers);
    }
}
