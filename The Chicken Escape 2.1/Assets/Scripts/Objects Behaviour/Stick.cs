using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private float range = 2f;
    private bool isCarrying = false;
    private bool firstTap = true;

    public bool isOnEscapeZone = false;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        if (distanceToPlayer > range)
            return;

        isCarrying = !isCarrying;
        if (isCarrying && firstTap)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            firstTap = false;
        }
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
