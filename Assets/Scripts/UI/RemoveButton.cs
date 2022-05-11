using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveButton : MonoBehaviour
{
    GameObject canvas;
    private void Start()
    {
        OnClickGrid.OnGridClicked += Deactivate;
        canvas = GameObject.Find("Canvas");
    }

    public void RemoveNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<UIDrag>().isSelected)
            {
                Destroy(node);
            }
        }
        GameObject[] curves = GameObject.FindGameObjectsWithTag("Curve");
        foreach (GameObject curve in curves)
        {
            if (curve.GetComponent<CubicBezier>().isSelected)
            {
                Destroy(curve);
            }
        }
        Deactivate();
    }

    public void Deactivate()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void Activate()
    {
        this.gameObject.GetComponent<Button>().interactable = true;
    }
}
