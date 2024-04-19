using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wood : MonoBehaviour
{
    private float range = 2f;
    private bool isPulled = false;
    private bool isCarrying = false;

    public bool isOnEscapeZone = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isCarrying || !isPulled)
            return;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + new Vector3(0, 0.76f);
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        if (distanceToPlayer > range)
            return;

        isCarrying = !isCarrying;
        CheckZone();
    }
    public void PullWood()
    {
        if (isPulled)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        if (distanceToPlayer > range)
            return;

        isPulled = true;
        gameObject.SetActive(true);
    }
    void CheckZone()
    {
        if (isCarrying)
            return;
        EscapeZone zone = FindObjectOfType<EscapeZone>();
        isOnEscapeZone = zone.IsOnZone(transform.position);
    }
}
