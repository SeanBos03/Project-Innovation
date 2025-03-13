using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class LoudnessRecorder : MonoBehaviour
{
    [SerializeField] int amountOfAudioSamples; //amount of data collected for detecting loudness
    [SerializeField] float threshold = 0.15f; // Set this in the Inspector
    AudioClip microphoneClip;
    [SerializeField] TextMeshProUGUI loudMess;

    public bool loudDetected = false;


    void Start()
    {
        microphoneClip = Microphone.Start(Microphone.devices[0], true, 20, AudioSettings.outputSampleRate);
    }

    void Update()
    {
        float volume = GetLoudness(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
        
        if (volume > threshold)
        {
            loudMess.text = "Loud detected";
            loudDetected = true;

            if (GameData.TurtorialStage == 7)
            {
                Invoke("Continue11", 2f);
            }
        }

        else
        {
            loudMess.text = "Loud not detected";
            loudDetected = false;
        }
    }

    void Continue11()
    {
        GameData.TurtorialStage = 8;
    }

    //clipPosition - position in the audio clip where we want to check the loudness
    public float GetLoudness(int clipPosition,AudioClip theAudioclip )
    {
        //we will take the clip position we want to look at then collect a small array of
        //data before the position, to the loud of some part of a clip
        int startPosition = clipPosition - amountOfAudioSamples;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[amountOfAudioSamples];
        theAudioclip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < amountOfAudioSamples; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / amountOfAudioSamples;
    }
}
