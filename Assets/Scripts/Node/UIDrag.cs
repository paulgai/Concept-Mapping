using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool isSelected = true;
    public bool isDragEnebled = true;
    public GameObject Anchors;
    public GameObject InOut;
    private float ScaleFactor;
    Vector3 dragOffset;
    private void Start()
    {
        OnClickGrid.OnGridClicked += DeactivateAnchors;
        ClickRoutine();
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
        ClickRoutine();
    }

    public void ClickRoutine()
    {
        Anchors.SetActive(true);
        for (int i = 0; i < InOut.transform.childCount; i++)
        {
            InOut.transform.GetChild(i).GetComponent<InOutPins>().SetColor();
        }
        isSelected = true;
        GameObject.FindGameObjectWithTag("Remove Button").GetComponent<RemoveButton>().Activate();
    }

    private void DeactivateAnchors()
    {
        isSelected = false;
        Anchors.SetActive(false);
        for (int i = 0; i < InOut.transform.childCount; i++)
        {
            InOut.transform.GetChild(i).GetComponent<InOutPins>().SetTransparent();
        }
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out worldPoint))
        {
            GetComponent<RectTransform>().position = worldPoint + dragOffset;
        }
    }


    /*  public void OnPointerEnter(PointerEventData eventData)
      {
          List<RaycastResult> results = new List<RaycastResult>();
          this.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

          if (results.Count > 0)
          {
              hitted = results[0].gameObject;
          }
          else
          {
              hitted = this.gameObject;
          }

      }
      public void OnPointerExit(PointerEventData eventData)
      {
          hitted = this.gameObject;
      }*/

}
