using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideshowWithFade : MonoBehaviour
{
    public Image slideImage; // Reference to the UI Image
    public Sprite[] slides;  // Array of slides
    public float fadeDuration = 1f;  // Time for fading in/out
    public float displayDuration = 3f;  // Time before changing slides

    private int currentIndex = 0;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = slideImage.GetComponent<CanvasGroup>(); // Get the Canvas Group
        if (slides.Length > 0)
        {
            slideImage.sprite = slides[currentIndex];
            StartCoroutine(SlideshowSequence());
        }
    }

    IEnumerator SlideshowSequence()
    {
        while (currentIndex < slides.Length)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeOut());

            currentIndex++;
            if (currentIndex < slides.Length)
            {
                slideImage.sprite = slides[currentIndex];
            }
        }

        EndCutscene();
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene Finished!");
        // Example: Load next scene or start gameplay
        // SceneManager.LoadScene("GameScene");
    }
}
