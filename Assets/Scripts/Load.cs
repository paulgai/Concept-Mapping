using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Load : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenFile();

    public void OnLoad()
    {
#if UNITY_WEBGL
        Debug.Log("WebGL");
        OpenFile();
#endif
    }
}
