using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class turtorialScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turtorialText;
    [SerializeField] List<Sprite> imageList = new List<Sprite>();
    [SerializeField] Image theImage;
    [SerializeField] TextMeshProUGUI rotationStatus;

    void Update()
    {
        if (GameData.inTurortrial)
        {
            switch (GameData.TurtorialStage)
            {
                case 0:
                    rotationStatus.gameObject.SetActive(false);
                    turtorialText.text = "Hold your phone still";
                    theImage.sprite = imageList[0];
                    GameData.TurtorialStage = 1;
                    break;
                case 2:
                    turtorialText.text = "Rotate your phone to eggscape!";
                    theImage.sprite = imageList[1];
                    GameData.TurtorialStage = 3;
                    break;
                case 4:
                    turtorialText.text = "zoom in and out to change the camera";
                    theImage.sprite = imageList[2];
                    GameData.TurtorialStage = 5;
                    break;
                case 6:
                    turtorialText.text = "Blow on your phone to eggscape!";
                    theImage.sprite = imageList[3];
                    GameData.TurtorialStage = 7;
                    break;
                case 8:
                    rotationStatus.gameObject.SetActive(true);
                    turtorialText.gameObject.SetActive(false);
                    theImage.gameObject.SetActive(false);
                    GameData.TurtorialStage = 9;
                    break;
            }
        }
    }
}
