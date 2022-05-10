using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InOutPins : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ArrowDirection { Up, Down, Left, Right };
    public ArrowDirection direction = ArrowDirection.Up;
    public GameObject Node;
    public GameObject Curve;
    public GameObject Pointer;
    Color color;
    public bool isActive = false;
    void Start()
    {
        color = this.GetComponent<Image>().color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Node.GetComponent<UIDrag>().isDragEnebled = false;
        this.GetComponent<Image>().color = new Color(color.r - 0.1f, color.g - 0.1f, color.b - 0.1f, 1);
        isActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Node.GetComponent<UIDrag>().isSelected == false)
        {
            Node.GetComponent<UIDrag>().isDragEnebled = true;
            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        }
        isActive = false;
    }

    public void SetColor()
    {
        this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        isActive = true;
    }

    public void SetTransparent()
    {
        this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        isActive = false;
    }
    GameObject _currentPointer;
    public void OnBeginDrag(PointerEventData eventData)
    {

        //if (direction == ArrowDirection.Up)
        //{
        _currentPointer = Instantiate(Pointer, pos(), Quaternion.identity);

        Curve.GetComponent<CubicBezier>().OnMouse = true;
        Curve.GetComponent<CubicBezier>().Anchor1 = this.gameObject;
        Curve.GetComponent<CubicBezier>().Anchor2 = _currentPointer;
        Instantiate(Curve, new Vector3(), Quaternion.identity);
        //}
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging...");
        _currentPointer.transform.position = pos();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end dragg");
    }

    private Vector3 pos()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
        return worldPosition;
    }

}
