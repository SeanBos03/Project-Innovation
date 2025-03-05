using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationShowDebug : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI theObjectMeasureText;
    [SerializeField] GameObject theObject;
    // Update is called once per frame
    void Update()
    {
        theObjectMeasureText.text = "Envrionment local rotation: " + theObject.transform.localRotation.eulerAngles;
    }
}
