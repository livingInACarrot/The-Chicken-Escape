using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammock : MonoBehaviour
{
    public Sprite oneSide;
    public Sprite flat;
    public Sprite ball;
    public Sprite onChick;

    private SpriteRenderer hammock;

    private float range = 1f;
    private bool isCarrying = false;
    private bool isOnGround = false;
    private bool isOneSide = false;
    private void Start()
    {
        hammock = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isCarrying)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        transform.position += new Vector3(0, 0.7f);
    }

    void OnMouseDown()
    {
        if (!isOnGround && !isCarrying)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        if (distanceToPlayer > range)
            return;

        isCarrying = !isCarrying;
        if (isCarrying)
        {
            hammock.sprite = ball;// onChick;
        }
        else
        {
            hammock.sprite = ball;
        }
    }
    public void LeftClick()
    {
        if (isCarrying || isOnGround)
            return;

        if (!isOneSide)
        {
            hammock.sprite = oneSide;
            isOneSide = true;
        }
        else
        {
            hammock.sprite = flat;
            hammock.flipX = !hammock.flipX;
            isOnGround = true;
            Collider2D collider = GetComponent<Collider2D>();
            collider.offset = Vector2.zero;
        }
    }
    public void RightClick()
    {
        if (isCarrying || isOnGround)
            return;

        if (!isOneSide)
        {
            hammock.sprite = oneSide;
            hammock.flipX = !hammock.flipX;
            isOneSide = true;
        }
        else
        {
            hammock.sprite = flat;
            isOnGround = true;
            Collider2D collider = GetComponent<Collider2D>();
            collider.offset = Vector2.zero;
        }
    }
}
