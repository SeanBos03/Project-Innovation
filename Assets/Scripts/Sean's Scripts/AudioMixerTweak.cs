using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioMixerTweak : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(GameData.gameVolume) * 20);
    }
}