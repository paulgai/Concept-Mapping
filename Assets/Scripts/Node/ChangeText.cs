using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour, IPointerClickHandler
{
    public GameObject Anchors;
    public GameObject InputField;
    int tap;

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;
        //Debug.Log("tap: " + tap);
        this.transform.parent.gameObject.GetComponent<UIDrag>().ClickRoutine();
        if (tap == 2)
        {
            InputField.GetComponent<TMP_InputField>().text = this.GetComponent<TextMeshProUGUI>().text;
            InputField.SetActive(true);
        }
    }

    public void LockInput(TMP_InputField input)
    {
        if (input.text.Length > 0)
        {
            this.GetComponent<TextMeshProUGUI>().text = InputField.GetComponent<TMP_InputField>().text;
            InputField.SetActive(false);
        }
        else if (input.text.Length == 0)
        {
            //Debug.Log("Main Input Empty");
        }
    }
}
