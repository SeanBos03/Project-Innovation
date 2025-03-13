using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] Slider timerSlider;
    [SerializeField] bool debug_disableTimer = false;

    [SerializeField] GameObject orbitCam;
    [SerializeField] GameObject viewRotationCam;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject theUi;
    [SerializeField] int secsBeforeStart = 10;


    bool gameCanStart = false;
    void Start()
    {
        orbitCam.SetActive(true);
        GameData.gameStarts = false;
        GameData.shouldRoate = true;
        timerSlider.maxValue = amountOfTimeSeconds;
        timerSlider.value = amountOfTimeSeconds;
        GameData.rotationReady = false;

        if (GameData.timeRanOut) //if player didnt suceed from time out
        {
            GameData.gameStarts = true;
            timerMessage.gameObject.SetActive(true);
            Invoke("DisableTimerMessage", amountOfTimeOutMessageSeconds);
            theUi.SetActive(true);
            orbitCam.SetActive(false);
            viewRotationCam.SetActive(true);
            playerCam.SetActive(true);
        }

        GameData.mainCamDeaultRotation = mainCam.transform.rotation;
        timerText.text = "Time: " + amountOfTimeSeconds;
        GameData.life = amountOfLives;
        GameData.lifeMax = amountOfLives;

        if (!GameData.gameStarts)
        {
            Invoke("GameOrbitTimer", secsBeforeStart);
        }

    }

    void Timercountdown()
    {
        amountOfTimeSeconds--;
        timerSlider.value = amountOfTimeSeconds;
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

    void GameStart()
    {
        if (!GameData.gameStarts)
        {
            GameData.gameStarts = true;
        }

        if (!debug_disableTimer)
        {
            InvokeRepeating("Timercountdown", 1f, 1f);
        }

        theUi.SetActive(true);
        orbitCam.SetActive(false);
        viewRotationCam.SetActive(true);
        playerCam.SetActive(true);

    }
    void GameOrbitTimer()
    {
        GameStart();
    }

    void Update()
    {
        if (!GameData.gameStarts)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                GameStart();
            }
        }

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
    }
}
