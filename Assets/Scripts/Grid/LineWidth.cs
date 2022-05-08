using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWidth : MonoBehaviour
{
    public float initWidth = 0;
    void Start()
    {
        Zoom.OnZoomChanges += RewidthLine;
    }

    private void RewidthLine(float size)
    {
        this.GetComponent<LineRenderer>().startWidth = (size / 5f) * initWidth;
        this.GetComponent<LineRenderer>().endWidth = (size / 5f) * initWidth;
    }
}
