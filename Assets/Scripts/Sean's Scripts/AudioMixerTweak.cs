using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioMixerTweak : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        audioMixer.SetFloat("MusicVolMasterume", Mathf.Log10(GameData.gameVolume) * 20);
    }
}