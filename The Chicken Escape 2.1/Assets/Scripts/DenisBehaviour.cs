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

    void Start()
    {
        animator = GetComponent<Animator>();
        HideAllPoints();
        way = routeToBarn[0].position;
        MirrorAnimation();
    }
    void Update()
    {
        hours = TimerClock.Hours();
        minutes = TimerClock.Minutes();

        if (!inProgress)
        {
            if (hours == 11 && minutes == 30)
            {
                //StartCoroutine(IPickEggs());
            }
            if (hours == 13 && minutes == 0)
            {
                StartCoroutine(IGoToBarn());
            }
            if (hours == 15 && minutes == 0)
            {
                StartCoroutine(IGoFromBarn());
            }
            if (hours == 17 && minutes == 0)
            {
                //StartCoroutine(IWaterGarden());
            }
            if (hours == 20 && minutes == 0)
            {
                //StartCoroutine(IBringChicksHome());
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
    IEnumerator IGoToBarn()
    {
        inProgress = true;
        yield return new WaitUntil(() => GoToBarn());
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
                currentDestination = 0;
                return true;
            }
            way = routeToBarn[currentDestination].position;
            MirrorAnimation();
        }
        return false;
    }
    IEnumerator IGoFromBarn()
    {
        inProgress = true;
        yield return new WaitUntil(() => GoFromBarn());
        inProgress = false;
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
                currentDestination = 0;
                return true;
            }
            way = routeToBarn[currentDestination].position;
            MirrorAnimation();
        }
        return false;
    }
}
