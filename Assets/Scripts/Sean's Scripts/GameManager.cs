using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int amountOfLives;
    [SerializeField] TextMeshProUGUI lifeAmountText;
    void Start()
    {
        GameData.life = amountOfLives;
    }
    void Update()
    {
        lifeAmountText.text = "Lives: " + GameData.life;
    }
}
