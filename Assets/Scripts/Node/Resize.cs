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

    float previewsHeight1 = 50;
    float previewsHeight2 = 50;
    float previewsWidth1 = 100;
    float previewsWidth2 = 100;
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
            float currentHeight1 = startHeight + (RectDist.y - StartY);
            if (currentHeight1 > 50 || previewsHeight1 < currentHeight1)
            {
                Node.GetComponent<RectTransform>().sizeDelta =
                                    new Vector2(
                                        Node.GetComponent<RectTransform>().sizeDelta.x,
                                        currentHeight1
                                    );
                Node.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    Node.GetComponent<RectTransform>().anchoredPosition.x,
                    StartPositionY + (RectDist.y - StartY) * 0.5f
                );
                previewsHeight1 = currentHeight1;
            }
        }

        if (direction == Direction.Down || direction == Direction.DownLeft || direction == Direction.DownRight)
        {
            float currentHeight2 = startHeight - (RectDist.y - StartY);
            if (currentHeight2 > 50 || previewsHeight2 < currentHeight2)
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
                previewsHeight2 = currentHeight2;
            }
        }
        if (direction == Direction.Right || direction == Direction.UpRight || direction == Direction.DownRight)
        {
            float currentWidth1 = startWidth + (RectDist.x - StartX);
            if (currentWidth1 > 100 || previewsWidth1 < currentWidth1)
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
                previewsWidth1 = currentWidth1;
            }

        }
        if (direction == Direction.Left || direction == Direction.UpLeft || direction == Direction.DownLeft)
        {
            float currentWidth2 = startWidth - (RectDist.x - StartX);
            if (currentWidth2 > 100 || previewsWidth2 < currentWidth2)
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
                previewsWidth2 = currentWidth2;
            }

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        Node.GetComponent<UIDrag>().isDragEnebled = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


}
