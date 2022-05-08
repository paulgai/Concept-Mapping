using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OnClickGrid : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler
{
    public float XY_Limit = 10;
    public delegate void ClickAction();
    public static event ClickAction OnGridClicked;
    private Vector3 dragOrigin;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnGridClicked != null)
            OnGridClicked();
    }
    private void Start()
    {
        //canvas = GameObject.Find("Canvas");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOrigin = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float X = Camera.main.transform.position.x;
        float Y = Camera.main.transform.position.y;
        float Z = Camera.main.transform.position.z;
        Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(eventData.position);
        if ((X < XY_Limit || difference.x < 0) &&
        (X > -XY_Limit || difference.x > 0) &&
        (Y < XY_Limit || difference.y < 0) &&
        (Y > -XY_Limit || difference.y > 0))
        {
            Camera.main.transform.position += difference;
        }

    }
}
