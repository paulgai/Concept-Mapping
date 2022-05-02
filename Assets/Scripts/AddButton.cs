using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    public GameObject Node;
    GameObject canvas;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    public void AddNode()
    {
        Instantiate(Node, canvas.transform);
    }
}
