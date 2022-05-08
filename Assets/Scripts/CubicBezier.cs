using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicBezier : MonoBehaviour
{
    public GameObject Anchor1;
    public GameObject Anchor2;
    const int NumberOfPoints = 20;
    const float dL = 0.2f;
    public Vector3[] P = new Vector3[4];
    private Vector3[] data = new Vector3[NumberOfPoints];
    LineRenderer lr;
    PolygonCollider2D pg2d;
    Vector3 LastPos1, LastPos2;

    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        pg2d = this.GetComponent<PolygonCollider2D>();
        for (int i = 0; i < NumberOfPoints; i++)
        {
            float t = Mathf.InverseLerp(0, NumberOfPoints, i);
            data[i] = B(t);
        }
        lr.positionCount = NumberOfPoints;
        lr.SetPositions(data);

        SetCollider();
    }

    // Update is called once per frame
    void Update()
    {
        //if (LastPos1 == )

        LastPos1 = Anchor1.transform.position;
        LastPos2 = Anchor2.transform.position;
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
        Debug.Log("mag = " + v.magnitude);
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
}
