using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EscapeZone : MonoBehaviour
{
    public float xmin;
    public float xmax;
    public float ymin;
    public float ymax;
    public float range = 6f;
    private Vector3 pos;

    public Button button;

    void Start()
    {
        xmin += range;
        xmax -= range;
        ymin += range;
        ymax -= range;
        button.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 1));
            pos = worldPosition;
            if (pos.x < xmin || pos.x > xmax || pos.y > ymax || pos.y < ymin)
            {
                button.transform.position = pos;
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }
    public void Click()
    {
        transform.position = pos;
        button.gameObject.SetActive(false);
    }
    public bool IsOnZone(Vector3 pos)
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col.bounds.min.x < pos.x && col.bounds.max.x > pos.x && col.bounds.min.y < pos.y && col.bounds.max.y > pos.y)
            return true;
        return false;
    }
}
