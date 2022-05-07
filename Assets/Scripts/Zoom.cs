using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour
{
    public float ScaleStart = 1.2f;
    public float ScaleEnd = 0.8f;
    // public GameObject grid1;
    // public GameObject grid2;
    float scroll = 1;
    float max = 8f;
    float min = 3f;
    float speed = 0.1f;
    float scale = 0;
    void Update()
    {
        scroll = Input.mouseScrollDelta.y;
        scale = Camera.main.orthographicSize;
        if (scroll != 0)
        {
            Debug.Log("scale:" + scale);
            scale -= scroll * speed;
            if (min <= scale && scale <= max)
            {
                Camera.main.orthographicSize = scale;
            }

            /*if (scale <= ScaleStart && scale >= ScaleEnd)
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
            }*/
        }

    }


    /* void SetAlpha(float alpha)
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
     }*/

}