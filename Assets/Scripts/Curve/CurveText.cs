using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CurveText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public GameObject InputField;

    Vector3 dragOffset;
    int tap;
    void Start()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("On Begin");
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out worldPoint);
        dragOffset = GetComponent<RectTransform>().position - worldPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag...");
        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;
        if (tap == 2)
        {
            SetActivateInputField(true);
        }
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out worldPoint))
        {
            GetComponent<RectTransform>().position = worldPoint + dragOffset;
            InputField.GetComponent<RectTransform>().position = worldPoint + dragOffset;
        }
    }

    public void LockInput(TMP_InputField input)
    {
        SetActivateInputField(false);
    }

    public void SetActivateInputField(bool isActive)
    {
        if (isActive)
        {
            InputField.GetComponent<TMP_InputField>().text = this.GetComponent<TextMeshProUGUI>().text;
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = InputField.GetComponent<TMP_InputField>().text;
        }
        InputField.SetActive(isActive);
    }
}
