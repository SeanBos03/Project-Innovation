using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManagerStartScreen1 : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider gyroRotationSpeedSlider;
    void Start()
    {
    }

    void Update()
    {
        GameData.gyroRotationSpeed = gyroRotationSpeedSlider.value;
        GameData.gameVolume = volumeSlider.value;
    }
}
