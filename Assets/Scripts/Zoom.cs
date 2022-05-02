using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour
{
    public float ScaleStart = 1.2f;
    public float ScaleEnd = 0.8f;
    public GameObject grid1;
    public GameObject grid2;
    float scroll = 1;
    float max = 2f;
    float min = 0.5f;
    float speed = 0.02f;
    float scale = 0;
    void Update()
    {
        scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            if (scroll > 0 && this.GetComponent<CanvasScaler>().scaleFactor < max)
            {
                this.GetComponent<CanvasScaler>().scaleFactor += scroll * speed;
            }
            if (scroll < 0 && this.GetComponent<CanvasScaler>().scaleFactor > min)
            {
                this.GetComponent<CanvasScaler>().scaleFactor += scroll * speed;
            }
            scale = this.GetComponent<CanvasScaler>().scaleFactor;
            if (scale <= ScaleStart && scale >= ScaleEnd)
            {
                SetAlpha(Mathf.InverseLerp(ScaleStart, ScaleEnd, scale));
            }
            else if (scale > ScaleStart)
            {
                SetAlpha(0);
            }
            else
            {
                SetAlpha(1);
            }
        }

    }


    void SetAlpha(float alpha)
    {
        grid1.GetComponent<Image>().color = new Color(
            grid1.GetComponent<Image>().color.r,
            grid1.GetComponent<Image>().color.g,
            grid1.GetComponent<Image>().color.b,
            alpha
            );
        grid2.GetComponent<Image>().color = new Color(
            grid2.GetComponent<Image>().color.r,
            grid2.GetComponent<Image>().color.g,
            grid2.GetComponent<Image>().color.b,
            1 - alpha
        );
    }

}