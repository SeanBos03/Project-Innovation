using UnityEngine;
using UnityEngine.UI;

public class ResizeImage : MonoBehaviour
{
    public Image imageToResize;  // Reference to the Image component
    public float width = 200f;   // New width in pixels
    public float height = 100f;  // New height in pixels

    void Start()
    {
        // Resize the Image to the specified width and height
        RectTransform rt = imageToResize.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
    }
}
