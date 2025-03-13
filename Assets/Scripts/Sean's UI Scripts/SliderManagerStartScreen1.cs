using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderManagerStartScreen1 : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider gyroRotationSpeedSlider;
    void Start()
    {
    }

    void Update()
    {
        GameData.gyroRotationSpeed = gyroRotationSpeedSlider.value;
        GameData.gameVolume = volumeSlider.value;
        audioMixer.SetFloat("MusicVolMasterume", Mathf.Log10(GameData.gameVolume) * 20);
    }
}
