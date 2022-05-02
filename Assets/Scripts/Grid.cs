using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Grid : MonoBehaviour, IPointerClickHandler
{
    public delegate void ClickAction();
    public static event ClickAction OnGridClicked;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnGridClicked != null)
            OnGridClicked();
    }
}
