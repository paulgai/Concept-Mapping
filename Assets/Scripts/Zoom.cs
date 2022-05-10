using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour
{
    public delegate void ZoomChanged(float size);
    public static event ZoomChanged OnZoomChanges;
    float scroll = 1;
    float max = 8f;
    float min = 3f;
    float speed = 0.1f;
    float size = 0;
    void Update()
    {
        scroll = Input.mouseScrollDelta.y;
        size = Camera.main.orthographicSize;
        if (scroll != 0)
        {
            //Debug.Log("scale:" + size);
            size -= scroll * speed;
            if (min <= size && size <= max)
            {
                Camera.main.orthographicSize = size;
                if (OnZoomChanges != null)
                    OnZoomChanges(size);
            }
        }

    }


}