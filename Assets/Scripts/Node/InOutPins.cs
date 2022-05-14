using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InOutPins : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ArrowDirection { Up, Down, Left, Right, Node };
    public ArrowDirection direction = ArrowDirection.Up;
    public Sprite BlurArrow;
    public GameObject Node;
    public GameObject Curve;
    GameObject currentCurve;
    public GameObject Pointer;
    Color color = new Color();
    public bool isActive = false;
    GameObject _currentPointer;
    GameObject empty;
    GameObject canvas;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        ColorUtility.TryParseHtmlString("#29B6F2", out color);
        this.GetComponent<Image>().color = color;
        if (direction == ArrowDirection.Down)
        {
            empty = new GameObject();
            empty.transform.parent = this.gameObject.transform;
            empty.transform.position = this.gameObject.transform.position + new Vector3(0, 0.5f, 0);
        }
        else if (direction == ArrowDirection.Up)
        {
            empty = new GameObject();
            empty.transform.parent = this.gameObject.transform;
            empty.transform.position = this.gameObject.transform.position + new Vector3(0, -0.5f, 0);
        }
        else if (direction == ArrowDirection.Right)
        {
            empty = new GameObject();
            empty.transform.parent = this.gameObject.transform;
            empty.transform.position = this.gameObject.transform.position + new Vector3(-0.5f, 0, 0);
        }
        else if (direction == ArrowDirection.Left)
        {
            empty = new GameObject();
            empty.transform.parent = this.gameObject.transform;
            empty.transform.position = this.gameObject.transform.position + new Vector3(0.5f, 0, 0);
        }
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
            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        }
        Node.GetComponent<UIDrag>().isDragEnebled = true;
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
    Ray ray;
    RaycastHit hit;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _currentPointer = Instantiate(Pointer, pos(), Quaternion.identity);
        Curve.GetComponent<CubicBezier>().OnMouse = true;
        Curve.GetComponent<CubicBezier>().Anchor1 = empty;
        Curve.GetComponent<CubicBezier>().direction1 = direction;
        Curve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Node;
        Curve.GetComponent<CubicBezier>().Anchor2 = _currentPointer;
        Curve.GetComponent<CubicBezier>().CurveTextCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        Curve.GetComponent<CubicBezier>().CurveTextCanvas.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        currentCurve = Instantiate(Curve, new Vector3(), Quaternion.identity);
    }
    List<RaycastResult> results;
    public void OnDrag(PointerEventData eventData)
    {
        _currentPointer.transform.position = pos();
        results = new List<RaycastResult>();
        canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        if (results.Count > 0 && results[0].gameObject != this.gameObject)
        {
            //Debug.Log(results[0].gameObject.tag);
            if (results[0].gameObject.tag == "Left")
            {
                currentCurve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Left;
            }
            else if (results[0].gameObject.tag == "Right")
            {
                currentCurve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Right;
            }
            else if (results[0].gameObject.tag == "Down")
            {
                currentCurve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Down;
            }
            else if (results[0].gameObject.tag == "Up")
            {
                currentCurve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Up;
            }

        }
        else
        {
            currentCurve.GetComponent<CubicBezier>().direction2 = ArrowDirection.Node;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        if (results.Count > 0 && results[0].gameObject != this.gameObject)
        {
            if (results[0].gameObject.tag == "Left" ||
                results[0].gameObject.tag == "Right" ||
                results[0].gameObject.tag == "Down" ||
                results[0].gameObject.tag == "Up")
            {
                currentCurve.GetComponent<CubicBezier>().Anchor2 = results[0].gameObject.GetComponent<InOutPins>().empty;
                currentCurve.GetComponent<CubicBezier>().isΜovingΒyΜouse = false;
                currentCurve.GetComponent<CubicBezier>().BlurArrowEnable();
                currentCurve = null;
                Destroy(_currentPointer);

                //_currentPointer.GetComponent<SpriteRenderer>().sprite = BlurArrow;
            }
            else
            {
                Destroy(_currentPointer);
                Destroy(currentCurve);
            }
        }
        else
        {
            Destroy(_currentPointer);
            Destroy(currentCurve);
        }
    }

    private Vector3 pos()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
        return worldPosition;
    }



}
