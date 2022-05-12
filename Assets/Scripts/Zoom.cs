using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour//, IDragHandler
{
    public delegate void ZoomChanged(float size);
    public static event ZoomChanged OnZoomChanges;
    float scroll = 1;
    float max = 10f;
    float min = 3f;
    float speed = 0.2f;
    float size = 0;
    void Update()
    {
        scroll = Input.mouseScrollDelta.y;
        size = Camera.main.orthographicSize;
        if (scroll != 0)
        {
            size -= scroll * speed;
            if (min <= size && size <= max)
            {
                Camera.main.orthographicSize = size;
                if (OnZoomChanges != null)
                    OnZoomChanges(size);
            }
        }

    }
    /*public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Hit ");
        List<RaycastResult> results = new List<RaycastResult>();
        this.GetComponent<GraphicRaycaster>().Raycast(eventData, results);
        if (results.Count > 0)
        {
            Debug.Log("Hit " + results[0].gameObject.name);
        }

    }*/
    public void Test()
    {
        Debug.Log("test");
    }
}