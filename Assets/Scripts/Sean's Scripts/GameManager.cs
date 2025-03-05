using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int amountOfLives;
    [SerializeField] TextMeshProUGUI lifeAmountText;
    [SerializeField] TextMeshProUGUI stickText;
    void Start()
    {
        GameData.life = amountOfLives;
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
}
