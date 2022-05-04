using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Resize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum Direction { Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft };
    public Direction direction = Direction.Up;
    public Texture2D cursor;
    GameObject Node;
    Vector2 cursorHotspot;
    bool isDragging = false;
    GameObject canvas;
    //private float ScaleFactor;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        Node = this.transform.parent.transform.parent.gameObject;
        cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Node.GetComponent<UIDrag>().isDragEnebled = false;
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Node.GetComponent<UIDrag>().isDragEnebled = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    float StartY, StartX, startWidth, StartPositionY;
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        //Debug.Log("isDragging: " + isDragging);
        Node.GetComponent<UIDrag>().isDragEnebled = false;
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
        Vector2 RectDist;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Node.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out RectDist);
        StartY = RectDist.y;
        startWidth = Node.GetComponent<RectTransform>().sizeDelta.y;
        Debug.Log("startWidth: " + startWidth);
        StartPositionY = Node.GetComponent<RectTransform>().anchoredPosition.y;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //ScaleFactor = canvas.GetComponent<CanvasScaler>().scaleFactor;

        if (direction == Direction.Up || direction == Direction.UpRight || direction == Direction.UpLeft)
        {
            Vector2 RectDist;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Node.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out RectDist);
            //Debug.Log("eventData.delta.y: " + eventData.delta.y);
            //Debug.Log("RectDist.y: " + RectDist.y);
            Node.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        Node.GetComponent<RectTransform>().sizeDelta.x,
                        startWidth + (RectDist.y - StartY)
                    );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x,
                StartPositionY + (RectDist.y - StartY) / 2
            );
        }
        /*
        if (direction == Direction.Down || direction == Direction.DownLeft || direction == Direction.DownRight)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        Node.GetComponent<RectTransform>().sizeDelta.x,
                        Node.GetComponent<RectTransform>().sizeDelta.y - eventData.delta.y * (1 / ScaleFactor)
                    );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x,
                Node.GetComponent<RectTransform>().anchoredPosition.y + eventData.delta.y * (1 / ScaleFactor) / 2
            );
        }
        if (direction == Direction.Right || direction == Direction.UpRight || direction == Direction.DownRight)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        Node.GetComponent<RectTransform>().sizeDelta.x + eventData.delta.x * (1 / ScaleFactor),
                        Node.GetComponent<RectTransform>().sizeDelta.y
                    );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x + eventData.delta.x * (1 / ScaleFactor) / 2,
                Node.GetComponent<RectTransform>().anchoredPosition.y
            );
        }
        if (direction == Direction.Left || direction == Direction.UpLeft || direction == Direction.DownLeft)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        Node.GetComponent<RectTransform>().sizeDelta.x - eventData.delta.x * (1 / ScaleFactor),
                        Node.GetComponent<RectTransform>().sizeDelta.y
                    );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x + eventData.delta.x * (1 / ScaleFactor) / 2,
                Node.GetComponent<RectTransform>().anchoredPosition.y
            );
        }
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        //Debug.Log("isDragging: " + isDragging);
        Node.GetComponent<UIDrag>().isDragEnebled = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


}
