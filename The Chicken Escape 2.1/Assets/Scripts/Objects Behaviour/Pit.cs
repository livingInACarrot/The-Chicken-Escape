using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    private Animator animator;
    private bool isAnimating = false;
    private bool isBucketUp = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        Animate();
    }
    public void Animate()
    {
        if (isAnimating)
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
            animator.SetTrigger("PlayPitIdleUp");
        else
            animator.SetTrigger("PlayPitIdleDown");
    }
}
