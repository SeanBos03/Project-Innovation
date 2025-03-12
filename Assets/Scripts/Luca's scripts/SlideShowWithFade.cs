using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SlideshowWithCrossfade : MonoBehaviour
{
    [SerializeField] Image image1;
    [SerializeField] Image image2;  //second image is for crossfade
    [SerializeField] Sprite[] slides; //array of slide images
    [SerializeField] float fadeDuration = 1f;  //fading transition time
    [SerializeField] float displayDuration = 3f;  //time each slide is fully visible
    [SerializeField] Vector2[] imageSizes; //Array to specify the width and height for each image

    int currentIndex = 0;
    bool isImage1Active = true;

    void Start()
    {
        if (slides.Length > 0)
        {
            //set the first image
            image1.sprite = slides[0];
            image1.canvasRenderer.SetAlpha(1f);
            image2.canvasRenderer.SetAlpha(0f);

            ResizeImage(image1, 0);
            StartCoroutine(SlideshowSequence());
        }
    }

    IEnumerator SlideshowSequence()
    {
        while (currentIndex < slides.Length - 1) //loop through all images
        {
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(CrossfadeToNextSlide());
        }

        //cutscene ends
        EndCutscene();
    }

    IEnumerator CrossfadeToNextSlide()
    {
        currentIndex++; //move to next slide

        //resize current image to the specified size in imageSizes (if exists)
        ResizeImage(isImage1Active ? image2 : image1, currentIndex);

        //determine which image is fading in and out
        Image fadingInImage = isImage1Active ? image2 : image1;
        Image fadingOutImage = isImage1Active ? image1 : image2;

        //set the next image on the fading-in Image
        fadingInImage.sprite = slides[currentIndex];
        fadingInImage.canvasRenderer.SetAlpha(0f); // Start completely transparent

        // Ensure fading-out image is fully visible before fade starts
        fadingOutImage.canvasRenderer.SetAlpha(1f);

        //crossfade transition
        fadingInImage.CrossFadeAlpha(1f, fadeDuration, false);
        fadingOutImage.CrossFadeAlpha(0f, fadeDuration, false);

        yield return new WaitForSeconds(fadeDuration); // Wait for transition

        isImage1Active = !isImage1Active; // Swap active image
    }

    void ResizeImage(Image image, int index)
    {
        // Ensure imageSizes has a size for the current image
        if (index < imageSizes.Length)
        {
            Vector2 newSize = imageSizes[index];

            RectTransform rt = image.GetComponent<RectTransform>();
            rt.sizeDelta = newSize; // Set the new width and height of the image
        }
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene Finished!");
        // SceneManager.LoadScene("NextScene");
    }
}
