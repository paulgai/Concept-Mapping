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
        Deactivate();
    }

    public void Deactivate()
    {
        //Debug.Log("Deactivate");
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void Activate()
    {
        //Debug.Log("Activate");
        this.gameObject.GetComponent<Button>().interactable = true;
    }
}
