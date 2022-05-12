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
        Vector3 pos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        Instantiate(Node, pos, Quaternion.identity, canvas.transform);
    }
}
