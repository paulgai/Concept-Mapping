using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CubicBezier : MonoBehaviour, IPointerClickHandler
{
    public GameObject Selected;
    public GameObject Text;
    public GameObject InputField;
    public GameObject BlurArrow;
    public GameObject CurveTextCanvas;
    [HideInInspector]
    public bool OnMouse = true;
    [HideInInspector]
    public bool isSelected = false;
    [HideInInspector]
    public bool isΜovingΒyΜouse = true;
    [HideInInspector]
    public GameObject Anchor1;
    [HideInInspector]
    public GameObject Anchor2;
    const int NumberOfPoints = 100;
    const float dL = 0.2f;
    [HideInInspector]
    public Vector3[] P = new Vector3[4];
    private Vector3[] data = new Vector3[NumberOfPoints];
    LineRenderer lr, lr2;
    PolygonCollider2D pg2d;
    Vector3 LastPos1, LastPos2;
    [HideInInspector]
    public InOutPins.ArrowDirection direction1, direction2;
    void Start()
    {
        GenerateCurve();
        LastPos1 = Anchor1.transform.position;
        LastPos2 = Anchor2.transform.position;
        OnClickGrid.OnGridClicked += disable;
    }

    private void OnDestroy()
    {
        OnClickGrid.OnGridClicked -= disable;
    }
    void Update()
    {
        if (Anchor1 == null || Anchor2 == null)
        {
            Destroy(this.gameObject);
        }
        try
        {
            if (LastPos1 != Anchor1.transform.position || LastPos2 != Anchor2.transform.position)
            {
                GenerateCurve();
            }

            LastPos1 = Anchor1.transform.position;
            LastPos2 = Anchor2.transform.position;
        }
        catch { Destroy(this.gameObject); }

    }
    const float dxy = 0.15f;
    const float Dxy = 2.0f;
    private void GenerateCurve()
    {
        P[0] = Anchor1.transform.position;
        if (direction1 == InOutPins.ArrowDirection.Down)
        {
            P[1] = P[0] + new Vector3(0, -Dxy, 0);
        }
        else if (direction1 == InOutPins.ArrowDirection.Up)
        {
            P[1] = P[0] + new Vector3(0, Dxy, 0);
        }
        else if (direction1 == InOutPins.ArrowDirection.Right)
        {
            P[1] = P[0] + new Vector3(Dxy, 0, 0);
        }
        else
        {
            P[1] = P[0] + new Vector3(-Dxy, 0, 0);
        }

        if (isΜovingΒyΜouse)
        {
            P[3] = Anchor2.transform.position;
            if (direction2 == InOutPins.ArrowDirection.Node)
            {
                P[2] = P[3];
            }
            else if (direction2 == InOutPins.ArrowDirection.Left)
            {
                P[2] = P[3] + new Vector3(-Dxy, 0, 0);
                BlurArrow.transform.position = P[3] + new Vector3(-dxy, 0, 0);
            }
            else if (direction2 == InOutPins.ArrowDirection.Right)
            {
                P[2] = P[3] + new Vector3(Dxy, 0, 0);
                BlurArrow.transform.position = P[3] + new Vector3(dxy, 0, 0);
            }
            else if (direction2 == InOutPins.ArrowDirection.Down)
            {
                P[2] = P[3] + new Vector3(0, -Dxy, 0);
                BlurArrow.transform.position = P[3] + new Vector3(0, -dxy, 0);
            }
            else if (direction2 == InOutPins.ArrowDirection.Up)
            {
                P[2] = P[3] + new Vector3(0, Dxy, 0);
                BlurArrow.transform.position = P[3] + new Vector3(0, dxy, 0);
            }
        }
        else
        {

            if (direction2 == InOutPins.ArrowDirection.Left)
            {
                P[3] = Anchor2.transform.position + new Vector3(-dxy, 0, 0);
                P[2] = P[3] + new Vector3(-Dxy, 0, 0);
                BlurArrow.transform.position = P[3];
                BlurArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction2 == InOutPins.ArrowDirection.Right)
            {
                P[3] = Anchor2.transform.position + new Vector3(dxy, 0, 0);
                P[2] = P[3] + new Vector3(Dxy, 0, 0);
                BlurArrow.transform.position = P[3];
                BlurArrow.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (direction2 == InOutPins.ArrowDirection.Down)
            {
                P[3] = Anchor2.transform.position + new Vector3(0, -dxy, 0);
                P[2] = P[3] + new Vector3(0, -Dxy, 0);
                BlurArrow.transform.position = P[3];
                BlurArrow.transform.rotation = Quaternion.Euler(0, 0, 90);

            }
            else if (direction2 == InOutPins.ArrowDirection.Up)
            {
                P[3] = Anchor2.transform.position + new Vector3(0, dxy, 0);
                P[2] = P[3] + new Vector3(0, Dxy, 0);
                BlurArrow.transform.position = P[3];
                BlurArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }



        lr = this.GetComponent<LineRenderer>();
        lr2 = this.gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        pg2d = this.GetComponent<PolygonCollider2D>();
        for (int i = 0; i < NumberOfPoints; i++)
        {
            float t = Mathf.InverseLerp(0, NumberOfPoints, i);
            data[i] = B(t);
        }
        lr.positionCount = NumberOfPoints;
        lr.SetPositions(data);

        lr2.positionCount = NumberOfPoints;
        lr2.SetPositions(data);

        int middle = NumberOfPoints / 2;
        if (this.transform.childCount >= 2)
        {
            this.transform.GetChild(1).GetComponent<RectTransform>().position = data[middle];
        }


        SetCollider();
    }

    private Vector3 B(float t)
    {
        return
        Mathf.Pow(1 - t, 3) * P[0] +
        3 * t * Mathf.Pow(1 - t, 2) * P[1] +
        3 * (1 - t) * Mathf.Pow(t, 2) * P[2] +
        Mathf.Pow(t, 3) * P[3];
    }

    void SetCollider()
    {
        List<Vector2> pg2d_points = new List<Vector2>();
        pg2d.pathCount = 1;
        for (int i = 0; i <= NumberOfPoints - 1; i++)
        {
            if (i < NumberOfPoints - 1)
            {
                Calculateneighboring(pg2d_points, lr.GetPosition(i), lr.GetPosition(i + 1), -90);
            }
            else
            {
                Calculateneighboring(pg2d_points, lr.GetPosition(i), lr.GetPosition(i - 1), 90);
            }
        }
        for (int i = NumberOfPoints - 1; i >= 0; i--)
        {
            if (i > 0)
            {
                Calculateneighboring(pg2d_points, lr.GetPosition(i), lr.GetPosition(i - 1), -90);
            }
            else
            {
                Calculateneighboring(pg2d_points, lr.GetPosition(0), lr.GetPosition(1), 90);
            }
        }
        pg2d.SetPath(0, pg2d_points);

    }

    void Calculateneighboring(List<Vector2> pg2d_points, Vector3 start, Vector3 end, float angle)
    {
        float dist = Vector3.Distance(start, end);
        float lamda = dL / dist;
        Vector3 v = lamda * (end - start);
        Vector3 rotate_v = start + rotate(v, angle);
        Vector2 add = rotate_v;
        pg2d_points.Add(add);
    }

    Vector3 rotate(Vector3 point, float angle)
    {
        Vector3 ret = new Vector3();
        float cos = Mathf.Cos(angle * Mathf.PI / 180f);
        float sin = Mathf.Sin(angle * Mathf.PI / 180f);
        ret.x = cos * point.x - sin * point.y;
        ret.y = sin * point.x + cos * point.y;
        ret.z = 0;
        return ret;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Click on curve");
        Selected.GetComponent<LineRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Remove Button").GetComponent<RemoveButton>().Activate();

        string txt = Text.GetComponent<TextMeshProUGUI>().text;
        if (txt.Length == 0 || string.IsNullOrWhiteSpace(txt))
        {
            InputField.GetComponent<TMP_InputField>().text = Text.GetComponent<TextMeshProUGUI>().text;
            InputField.SetActive(true);
        }
        isSelected = true;

    }

    public void disable()
    {
        Selected.GetComponent<LineRenderer>().enabled = false;
        Text.GetComponent<TextMeshProUGUI>().text = InputField.GetComponent<TMP_InputField>().text;
        InputField.SetActive(false);
        string txt = Text.GetComponent<TextMeshProUGUI>().text;
        if (txt.Length == 0 || string.IsNullOrWhiteSpace(txt))
        {
            Text.SetActive(false);
        }
        else
        {
            Text.SetActive(true);
        }
        isSelected = false;
    }

    public void BlurArrowEnable()
    {
        BlurArrow.GetComponent<SpriteRenderer>().enabled = true;
    }

}

