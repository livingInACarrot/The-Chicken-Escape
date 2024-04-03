using System;
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
    private bool inProgress = false;
    private bool isStanding = false;

    public List<Transform> routePickEggs;
    public List<Transform> routeToBarn;
    public List<Transform> routeFromBarn;
    public List<Transform> routeWaterGarden;
    public List<Transform> routeBringChicksHome;
    public List<Transform> routeFromCoop;
    private int currentDestination = 0;
    private Vector2 way;

    public GameObject wateringCan;

    void Start()
    {
        animator = GetComponent<Animator>();
        HideAllPoints();
    }
    void Update()
    {
        hours = TimerClock.Hours();
        minutes = TimerClock.Minutes();

        if (!inProgress)
        {
            currentDestination = 0;
            if (hours == 11 && minutes == 30)
            {
                way = routePickEggs[0].position;
                MirrorAnimation();
                //StartCoroutine(GoSomeWhere(PickEggs()));
            }
            if (hours == 13 && minutes == 0)
            {
                way = routeToBarn[0].position;
                MirrorAnimation();
                StartCoroutine(GoSomeWhere(GoToBarn()));
            }
            if (hours == 15 && minutes == 0)
            {
                way = routeFromBarn[0].position;
                MirrorAnimation();
                StartCoroutine(GoSomeWhere(GoFromBarn()));
            }
            if (hours == 17 && minutes == 0)
            {
                way = routeWaterGarden[0].position;
                MirrorAnimation();
                //StartCoroutine(GoSomeWhere(WaterGarden()));
            }
            if (hours == 20 && minutes == 0)
            {
                way = routeBringChicksHome[0].position;
                MirrorAnimation();
                //StartCoroutine(GoSomeWhere(BringChicksHome()));
            }
        }
    }
    private void HideAllPoints()
    {
        HideAllPoints(ref routePickEggs);
        HideAllPoints(ref routeToBarn);
        HideAllPoints(ref routeFromBarn);
        HideAllPoints(ref routeWaterGarden);
        HideAllPoints(ref routeBringChicksHome);
    }
    private void HideAllPoints(ref List<Transform> list)
    {
        foreach (var item in list) 
        {
            item.gameObject.SetActive(false);
        }
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
        if (isStanding)
            return "back_stand";
        else if (Mathf.Abs(way.x - transform.position.x) < Mathf.Abs(way.y - transform.position.y) &&
            transform.position.y < way.y)
            return "back_walk";
        else if (Mathf.Abs(way.x - transform.position.x) < Mathf.Abs(way.y - transform.position.y) &&
            transform.position.y >= way.y)
            return "towards_walk";
        else
            return "side_walk";
    }
    IEnumerator GoSomeWhere(bool func)
    {
        inProgress = true;
        yield return new WaitUntil(() => func);
        inProgress = false;
    }
    bool GoToBarn()
    {
        animator.Play(ChooseAnimation());
        transform.position = Vector2.MoveTowards(transform.position, way, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, way) < range)
        {
            ++currentDestination;
            if (currentDestination >= routeToBarn.Count)
            {
                return true;
            }
            way = routeToBarn[currentDestination].position;
            MirrorAnimation();
        }
        return false;
    }
    bool GoFromBarn()
    {
        animator.Play(ChooseAnimation());
        transform.position = Vector2.MoveTowards(transform.position, way, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, way) < range)
        {
            ++currentDestination;
            if (currentDestination >= routeFromBarn.Count)
            {
                return true;
            }
            way = routeFromBarn[currentDestination].position;
            MirrorAnimation();
        }
        return false;
    }
    bool WaterGarden()
    {
        animator.Play(ChooseAnimation());
        transform.position = Vector2.MoveTowards(transform.position, way, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, way) < range)
        {
            // Denis reached the watering can
            if (currentDestination >= 5)
            {
                // Now watering can will be in his hand
                Vector3 DenisBottom = transform.position;
                RectTransform canRectTransform = wateringCan.GetComponent<RectTransform>();
                canRectTransform.position = DenisBottom;
                canRectTransform.anchoredPosition += new Vector2(40, 100);
            }

            ++currentDestination;
            if (currentDestination >= routeFromBarn.Count)
            {
                return true;
            }
            way = routeWaterGarden[currentDestination].position;
            MirrorAnimation();
        }
        return false;
    }
}
