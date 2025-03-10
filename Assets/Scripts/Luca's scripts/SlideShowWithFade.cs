using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideshowWithCrossfade : MonoBehaviour
{
    public Image image1;  // First UI Image
    public Image image2;  // Second UI Image (used for crossfade)

    public Sprite[] slides;  // Array of slide images
    public float fadeDuration = 1f;  // Time for fading transition
    public float displayDuration = 3f;  // Time each slide is fully visible

    // New: Arrays to specify the width and height for each image
    public Vector2[] imageSizes; // New: Sizes for each image (set width and height for each image)

    private int currentIndex = 0;
    private bool isImage1Active = true;

    void Start()
    {
        if (slides.Length > 0)
        {
            // Set the first image
            image1.sprite = slides[0];
            image1.canvasRenderer.SetAlpha(1f);  // Fully visible
            image2.canvasRenderer.SetAlpha(0f);  // Fully transparent

            // Resize image1 according to the first entry in imageSizes (optional)
            ResizeImage(image1, 0);

            StartCoroutine(SlideshowSequence());
        }
    }

    IEnumerator SlideshowSequence()
    {
        while (currentIndex < slides.Length - 1) // Loop through all images
        {
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(CrossfadeToNextSlide());
        }

        EndCutscene(); // Call function when finished
    }

    IEnumerator CrossfadeToNextSlide()
    {
        currentIndex++; // Move to next slide

        // Resize current image to the specified size in imageSizes (if exists)
        ResizeImage(isImage1Active ? image2 : image1, currentIndex);

        // Determine which image is fading in and out
        Image fadingInImage = isImage1Active ? image2 : image1;
        Image fadingOutImage = isImage1Active ? image1 : image2;

        // Set the next image on the fading-in Image
        fadingInImage.sprite = slides[currentIndex];
        fadingInImage.canvasRenderer.SetAlpha(0f); // Start completely transparent

        // Ensure fading-out image is fully visible before fade starts
        fadingOutImage.canvasRenderer.SetAlpha(1f);

        // Crossfade transition
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
        // SceneManager.LoadScene("NextScene"); // Uncomment if you want to load a new scene
    }
}
