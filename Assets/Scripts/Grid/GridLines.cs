using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour
{
    public GameObject GridLine;
    public float ThinLineWidth = 0.02f;
    public float ThickLineWidth = 0.1f;
    private float ThickLineStep;
    public float ThinLineStep = 1;
    public int HalfSteps = 1;
    LineRenderer lr;

    void Start()
    {
        ThickLineStep = 5 * ThinLineStep;
        float w = (2 * HalfSteps + 1) * ThickLineStep;
        //vertical thick
        for (int i = -HalfSteps; i <= HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThickLineWidth;
            lr.endWidth = ThickLineWidth;
            GridLine.GetComponent<LineWidth>().initWidth = ThickLineWidth;
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
            GridLine.GetComponent<LineWidth>().initWidth = ThickLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(-w * 0.5f, i * ThickLineStep, 0));
            lr.SetPosition(1, new Vector3(w * 0.5f, i * ThickLineStep, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
        w = (2 * 5 * HalfSteps + 1) * ThinLineStep;
        //vertical thin
        for (int i = -5 * HalfSteps; i <= 5 * HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThinLineWidth;
            lr.endWidth = ThinLineWidth;
            GridLine.GetComponent<LineWidth>().initWidth = ThinLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(i * ThinLineStep, -w * 0.5f, 0));
            lr.SetPosition(1, new Vector3(i * ThinLineStep, w * 0.5f, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }
        //horizontal thin
        for (int i = -5 * HalfSteps; i <= 5 * HalfSteps; i++)
        {
            lr = GridLine.GetComponent<LineRenderer>();
            lr.startWidth = ThinLineWidth;
            lr.endWidth = ThinLineWidth;
            GridLine.GetComponent<LineWidth>().initWidth = ThinLineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(-w * 0.5f, i * ThinLineStep, 0));
            lr.SetPosition(1, new Vector3(w * 0.5f, i * ThinLineStep, 0));
            Instantiate(GridLine, Vector3.zero, Quaternion.identity, this.transform);
        }

    }



}
