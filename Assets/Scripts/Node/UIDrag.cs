using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public bool isDragEnebled = true;

    public GameObject Anchors;
    private float ScaleFactor;
    GameObject canvas;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        Grid.OnGridClicked += DeactivateAnchors;
    }
    private void OnDestroy()
    {
        Grid.OnGridClicked -= DeactivateAnchors;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragEnebled)
        {
            //ScaleFactor = canvas.GetComponent<CanvasScaler>().scaleFactor;
            ScaleFactor = Camera.main.orthographicSize;
            this.GetComponent<RectTransform>().anchoredPosition += eventData.delta * 1280 / 100;//(ScaleFactor / 5);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Anchors.SetActive(true);
    }

    private void DeactivateAnchors()
    {
        Anchors.SetActive(false);
    }
}
