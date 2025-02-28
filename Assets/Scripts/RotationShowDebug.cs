using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationShowDebug : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI daText;
    [SerializeField] GameObject theObject;
    // Update is called once per frame
    void Update()
    {
        daText.text = "Envrionment local rotation: " + theObject.transform.localRotation.eulerAngles;
    }
}
