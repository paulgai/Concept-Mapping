using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler
{
    public bool isSelected = true;
    public delegate void ClickAction();
    public static event ClickAction OnNodeClicked;
    public bool isDragEnebled = true;
    public GameObject Anchors;
    private float ScaleFactor;
    Vector3 dragOffset;
    private void Start()
    {
        OnClickGrid.OnGridClicked += DeactivateAnchors;
    }
    private void OnDestroy()
    {
        OnClickGrid.OnGridClicked -= DeactivateAnchors;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out worldPoint);
        dragOffset = GetComponent<RectTransform>().position - worldPoint;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragEnebled)
        {
            SetDraggedPosition(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Anchors.SetActive(true);
        isSelected = true;
        if (OnNodeClicked != null)
            OnNodeClicked();
    }

    private void DeactivateAnchors()
    {
        isSelected = false;
        Anchors.SetActive(false);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out worldPoint))
        {
            GetComponent<RectTransform>().position = worldPoint + dragOffset;
        }
    }
}
