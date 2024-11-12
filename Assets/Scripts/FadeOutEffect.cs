using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutEffect : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2.0f;
    public float delayBeforeFade = 3.0f;
    private float elapsedTime = 0f;

    private void Start()
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("FadeOutEffect: fadeImage is not assigned in the inspector.");
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alphaValue);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }
}