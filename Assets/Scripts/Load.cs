using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;
using System;

public class Load : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string OpenFile();

    public GameObject LoadText;

    public void OnLoad()
    {
#if UNITY_WEBGL
        Debug.Log("WebGL");
        try
        {
            string str = OpenFile();
            LoadText.GetComponent<TextMeshProUGUI>().text = str;
        }
        catch (Exception ex)
        {
            LoadText.GetComponent<TextMeshProUGUI>().text = ex.Message;
        }

#endif
    }
}
