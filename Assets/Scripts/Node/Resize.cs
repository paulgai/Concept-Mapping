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
    float StartY, StartX, startWidth, startHeight, StartPositionX, StartPositionY;
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        Node.GetComponent<UIDrag>().isDragEnebled = false;
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
        Vector2 RectDist;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out RectDist
            );
        StartY = RectDist.y;
        StartX = RectDist.x;
        startWidth = Node.GetComponent<RectTransform>().sizeDelta.x;
        startHeight = Node.GetComponent<RectTransform>().sizeDelta.y;
        StartPositionX = Node.GetComponent<RectTransform>().anchoredPosition.x;
        StartPositionY = Node.GetComponent<RectTransform>().anchoredPosition.y;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 RectDist;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out RectDist
            );
        if (direction == Direction.Up || direction == Direction.UpRight || direction == Direction.UpLeft)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                     new Vector2(
                         Node.GetComponent<RectTransform>().sizeDelta.x,
                         startHeight + (RectDist.y - StartY)
                     );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x,
                StartPositionY + (RectDist.y - StartY) * 0.5f
            );
        }

        if (direction == Direction.Down || direction == Direction.DownLeft || direction == Direction.DownRight)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        Node.GetComponent<RectTransform>().sizeDelta.x,
                        startHeight - (RectDist.y - StartY)
                    );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                Node.GetComponent<RectTransform>().anchoredPosition.x,
                StartPositionY + (RectDist.y - StartY) * 0.5f
            );

        }
        if (direction == Direction.Right || direction == Direction.UpRight || direction == Direction.DownRight)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                     new Vector2(
                         startWidth + (RectDist.x - StartX),
                         Node.GetComponent<RectTransform>().sizeDelta.y

                     );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                StartPositionX + (RectDist.x - StartX) * 0.5f,
                Node.GetComponent<RectTransform>().anchoredPosition.y
            );
        }
        if (direction == Direction.Left || direction == Direction.UpLeft || direction == Direction.DownLeft)
        {
            Node.GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    startWidth - (RectDist.x - StartX),
                    Node.GetComponent<RectTransform>().sizeDelta.y

                );
            Node.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                StartPositionX + (RectDist.x - StartX) * 0.5f,
                Node.GetComponent<RectTransform>().anchoredPosition.y
            );
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        //Debug.Log("isDragging: " + isDragging);
        Node.GetComponent<UIDrag>().isDragEnebled = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


}
