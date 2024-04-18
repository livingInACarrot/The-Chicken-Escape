using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private float range = 2f;
    private bool isCarrying = false;

    public Sprite onGround;
    public Sprite onChick;

    private SpriteRenderer rope;

    public bool isOnEscapeZone = false;

    private void Start()
    {
        rope = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isCarrying)
            return;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        transform.position += new Vector3(0, 0.6f);
    }

    void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer > range)
            return;

        isCarrying = !isCarrying;
        if (isCarrying)
        {
            rope.sprite = onGround; // onChick;
            Collider2D collider = GetComponent<Collider2D>();
            collider.offset = Vector2.zero;
        }
        else
            rope.sprite = onGround;
        CheckZone();
    }
    void CheckZone()
    {
        if (isCarrying)
            return;
        EscapeZone zone = FindObjectOfType<EscapeZone>();
        isOnEscapeZone = zone.IsOnZone(transform.position);
    }
}
