using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour
{
    public GameObject GridLine;
    public float ThinLineWidth = 0.02f;
    public float ThickLineWidth = 0.1f;
    private float ThinLineStep = 5;
    public float ThickLineStep = 1;
    public int HalfSteps = 1;
    LineRenderer lr;

    void Start()
    {
        ThinLineStep = 5 * ThickLineStep;
        float w = (2 * HalfSteps + 1) * ThickLineStep;
        //vertical thick
        for (int i = -HalfSteps; i <= HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThickLineWidth;
            lr.endWidth = ThickLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(i * ThickLineStep, -w * 0.5f, 0));
            lr.SetPosition(1, new Vector3(i * ThickLineStep, w * 0.5f, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
        //horizontal thick
        for (int i = -HalfSteps; i <= HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThickLineWidth;
            lr.endWidth = ThickLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(-w * 0.5f, i * ThickLineStep, 0));
            lr.SetPosition(1, new Vector3(w * 0.5f, i * ThickLineStep, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
        w = (2 * HalfSteps + 1) * ThinLineStep;
        //vertical thin
        for (int i = -HalfSteps; i <= HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThinLineWidth;
            lr.endWidth = ThinLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(i * ThinLineStep, -w * 0.5f, 0));
            lr.SetPosition(1, new Vector3(i * ThinLineStep, w * 0.5f, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
        //horizontal thin
        for (int i = -HalfSteps; i <= HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThinLineWidth;
            lr.endWidth = ThinLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(-w * 0.5f, i * ThinLineStep, 0));
            lr.SetPosition(1, new Vector3(w * 0.5f, i * ThinLineStep, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
    }

}
