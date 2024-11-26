using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform targetImage; // The image to scale
    [SerializeField] private Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale when hovered
    [SerializeField] private float scaleSpeed = 0.2f; // Speed of scaling animation

    private Vector3 originalScale;

    private void Start()
    {
        // Save the original scale of the image
        if (targetImage == null)
            targetImage = GetComponent<RectTransform>();

        originalScale = targetImage.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines(); // Stop any ongoing scaling animations
        StartCoroutine(ScaleImage(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines(); // Stop any ongoing scaling animations
        StartCoroutine(ScaleImage(originalScale));
    }

    private System.Collections.IEnumerator ScaleImage(Vector3 targetScale)
    {
        Vector3 currentScale = targetImage.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < scaleSpeed)
        {
            elapsedTime += Time.deltaTime;
            targetImage.localScale = Vector3.Lerp(currentScale, targetScale, elapsedTime / scaleSpeed);
            yield return null;
        }

        targetImage.localScale = targetScale; // Ensure exact target scale is applied
    }
}
