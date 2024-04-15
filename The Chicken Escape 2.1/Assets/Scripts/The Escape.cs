using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEscape : MonoBehaviour
{
    public float xmin;
    public float xmax;
    public float ymin;
    public float ymax;
    public float range = 1f;
    private void Update()
    {
        if (ReadyForEscape4())
        {
            Debug.Log("Ready for Escape");
        }
    }
    bool ReadyForEscape4()
    {
        // 2 woods, 1 bucket, 1 rope
        Wood[] woods = FindObjectsOfType<Wood>();
        int woodsCount = 0;
        foreach (var wood in woods)
        {
            if (wood.isOnEscapeZone)
                ++woodsCount;
        }
        if (woodsCount < 2)
            return false;
        if (FindObjectOfType<Rope>().isOnEscapeZone && FindObjectOfType<Bucket>().isOnEscapeZone)
            return true;
        return false;
    }
}
