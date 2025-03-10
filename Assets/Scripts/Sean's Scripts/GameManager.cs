using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] int amountOfTimeOutMessageSeconds = 3;
    [SerializeField] int amountOfTimeSeconds = 30;
    [SerializeField] int amountOfLives;
    [SerializeField] TextMeshProUGUI timerMessage;
    [SerializeField] TextMeshProUGUI lifeAmountText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI stickText;
    [SerializeField] bool debug_disableTimer = false;
    void Start()
    {
        if (GameData.timeRanOut)
        {
            timerMessage.gameObject.SetActive(true);
            Invoke("DisableTimerMessage", amountOfTimeOutMessageSeconds);
        }

        GameData.mainCamDeaultRotation = mainCam.transform.rotation;
        timerText.text = "Time: " + amountOfTimeSeconds;
        GameData.life = amountOfLives;
        GameData.lifeMax = amountOfLives;

        if (!debug_disableTimer)
        {
            InvokeRepeating("Timercountdown", 1f, 1f);
        }
        
    }

    void Timercountdown()
    {
        amountOfTimeSeconds--;
        timerText.text = "Time: " + amountOfTimeSeconds;

        if (amountOfTimeSeconds <= 0)
        {
            SceneReload sceneReload = GetComponent<SceneReload>();
            GameData.timeRanOut = true;
            sceneReload.RestartScene();
        }
    }

    void DisableTimerMessage()
    {
        GameData.timeRanOut = false;
        timerMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        lifeAmountText.text = "Lives: " + GameData.life;

        if (GameData.shouldStick)
        {
            stickText.text = "Stick: ON";
        }

        else
        {
            stickText.text = "Stick: OFF";
            return;
        }
    }
    public void ToggleStick()
    {
        GameData.shouldStick = !GameData.shouldStick;
    }

    public void ResetCam()
    {
        mainCam.transform.rotation = GameData.mainCamDeaultRotation;
        Debug.Log(GameData.mainCamDeaultRotation.eulerAngles);
    }
}
