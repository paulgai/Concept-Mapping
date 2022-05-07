using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveButton : MonoBehaviour
{
    GameObject canvas;
    private void Awake()
    {
        Debug.Log("Remove button start");
        OnClickGrid.OnGridClicked += Deactivate;
        UIDrag.OnNodeClicked += Activate;
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
        Deactivate();
    }

    private void Deactivate()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
    }

    private void Activate()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
    }
}
