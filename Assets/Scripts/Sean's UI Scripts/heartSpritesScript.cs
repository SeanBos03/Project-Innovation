using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class heartSpritesScript : MonoBehaviour
{
    [SerializeField] List<Image> heartImages = new List<Image>();
    [SerializeField] Sprite fullHeartSprite;
    [SerializeField] Sprite emptyHeartSprite;

    void Start()
    {
    }
    void Update()
    {
        foreach (Image image in heartImages)
        {
            image.sprite = emptyHeartSprite;
        }

        for (int i = 0; i < GameData.life; i++)
        {
            heartImages[i].sprite = fullHeartSprite;
        }

        //for (int i = GameData.lifeMax; i > 0; i--)
        //{
            
            
        //}
    }
}
