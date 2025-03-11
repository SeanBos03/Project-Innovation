using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFill : MonoBehaviour
{
    Slider theSlider;
    Transform fill;
    void Start()
    {
        theSlider = GetComponent<Slider>();
        fill = transform.Find("Fill Area/Fill");
    }

    void Update()
    {
        if (theSlider.value == theSlider.minValue)
        {
            
            fill.gameObject.SetActive(false);
        }

        else
        {
            fill.gameObject.SetActive(true);
        }
    }
}
