using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pit : MonoBehaviour
{
    private float range = 5f;

    private Animator animator;
    private bool isAnimating = false;
    private bool isBucketUp = false;
    private bool noBucket = false;
    private bool noRope = false;

    public GameObject bucket;
    public GameObject rope;

    private Button buckBut;
    private Button ropeBut;
    void Start()
    {
        animator = GetComponent<Animator>();
        buckBut = GetComponentsInChildren<Button>()[0];
        ropeBut = GetComponentsInChildren<Button>()[1];
        buckBut.gameObject.SetActive(false);
        ropeBut.gameObject.SetActive(false);
        bucket.SetActive(false);
        rope.SetActive(false);
    }

    void OnMouseDown()
    {
        Animate();
    }
    public void Animate()
    {
        if (!SmallDistanceToPlayer())
            return;

        if (isAnimating || noBucket || noRope)
            return;

        isAnimating = true;
        if (isBucketUp)
            StartCoroutine(PlayAnimation("pit"));
        else
            StartCoroutine(PlayAnimation("pit 1"));
    }
    IEnumerator PlayAnimation(string name)
    {
        animator.SetTrigger("PlayPit");
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            yield return null;
        }
        isAnimating = false;
        isBucketUp = !isBucketUp;
        if (isBucketUp)
        {
            animator.SetTrigger("PlayPitIdleUp");
            buckBut.gameObject.SetActive(true);
        }
 
        else
        {
            animator.SetTrigger("PlayPitIdleDown");
            buckBut.gameObject.SetActive(false);
        }
    }
    public void ClickOnBucket()
    {
        if (!SmallDistanceToPlayer())
            return;

        animator.SetTrigger("PlayPitNoBucket");
        buckBut.gameObject.SetActive(false);
        ropeBut.gameObject.SetActive(true);
        noBucket = true;
        bucket.SetActive(true);
    }
    public void ClickOnRope()
    {
        if (!SmallDistanceToPlayer())
            return;

        animator.SetTrigger("PlayPitNoRope");
        ropeBut.gameObject.SetActive(false);
        noRope = true;
        rope.SetActive(true);
    }
    private bool SmallDistanceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer > range)
            return false;
        return true;
    }
}
