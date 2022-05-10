using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowAlpha : MonoBehaviour
{
    public bool isActive = false;
    Color color;
    private void Start()
    {
        color = this.GetComponent<Image>().color;
    }
    public void SetTransparent()
    {
        this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        isActive = false;
    }

    public void SetColor()
    {
        this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        isActive = true;
    }
}
