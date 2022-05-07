using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OnClickGrid : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public delegate void ClickAction();
    public static event ClickAction OnGridClicked;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnGridClicked != null)
            OnGridClicked();
    }
    private void Start()
    {
        //canvas = GameObject.Find("Canvas");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //ScaleFactor = canvas.GetComponent<CanvasScaler>().scaleFactor;
        // Vector2 Dxy = eventData.delta * (1 / ScaleFactor);
        // Camera.main.transform.position += new Vector3(Dxy.x, Dxy.y, Camera.main.transform.position.z);
    }
}
