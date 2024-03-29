using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenisBehaviour : MonoBehaviour
{
    private Animator animator;
    private int hours;
    private int minutes;
    public float speed = 3f;
    private float range = 0.1f;
    private bool block = true;

    // Points to visit on map
    public Transform home;
    public Transform nearHome;
    public Transform betweenHomeAndBarn;
    public Transform leftFromBarn;
    public Transform nearBarn;
    public Transform barn;
    public Transform pit;
    public Transform point1;
    public Transform point2;
    public Transform bottomGarden;
    public Transform hutFence;
    public Transform hutEntrance;
    public Transform topRightBed;

    private int currentDestination = 0;
    private List<Transform> routeToBarn;
    private Vector2 way;

    void Start()
    {
        animator = GetComponent<Animator>();
        HideAllPoints();
        routeToBarn = new () { home, nearHome, betweenHomeAndBarn, leftFromBarn, nearBarn, barn};
        way = routeToBarn[0].position;
        MirrorAnimation();
    }

    void Update()
    {
        hours = TimerClock.Hours();
        minutes = TimerClock.Minutes();

        GoToBarn();

        if (!block)
        {
            if (hours == 11 && minutes == 30)
            {
                GoToBarn();
            }
            if (hours == 13 && minutes == 0)
            {
                //StartCoroutine(GoToBarn());
                //GoToBarn();
            }
            if (hours == 15 && minutes == 0)
            {

            }
            if (hours == 17 && minutes == 0)
            {

            }
            if (hours == 20 && minutes == 0)
            {

            }
        }

    }
    private void HideAllPoints()
    {
        //home.gameObject.SetActive(false);
        // ect.
    }
    private void MirrorAnimation()
    {
        if (way.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (way.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);

    }
    private string ChooseAnimation()
    {
        if (Mathf.Abs(way.x - transform.position.x) < Mathf.Abs(way.y - transform.position.y) &&
            transform.position.y < way.y)
            return "back_walk";
        else if (Mathf.Abs(way.x - transform.position.x) < Mathf.Abs(way.y - transform.position.y) &&
            transform.position.y >= way.y)
            return "towards_walk";
        else
            return "side_walk";
    }
    void GoToBarn()
    {
        //block = true;
        animator.Play(ChooseAnimation());
        transform.position = Vector2.MoveTowards(transform.position, way, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, way) < range)
        {
            currentDestination += 1;
            if (currentDestination >= routeToBarn.Count)
                return;
            way = routeToBarn[currentDestination].position;
            MirrorAnimation();
        }
    }
}
