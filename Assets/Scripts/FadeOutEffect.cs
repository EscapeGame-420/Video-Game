using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutEffect : MonoBehaviour
{
    public Image fadeImage;
    public Image crosshair;
    public GameObject toolbar;
    public JulieMovement julieMovement;
    public JulieVisionFollow julieVisionFollow;
    public float fadeDuration = 2.0f;
    public float delayBeforeFade = 2.0f;

    private float elapsedTime = 0f;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 1f);
            StartCoroutine(FadeOut());
        }

        if (crosshair != null)
        {
            crosshair.enabled = false;
        }

        if (toolbar != null)
        {
            toolbar.SetActive(false);
        }

        if (julieMovement != null)
        {
            julieMovement.enabled = false;
        }

        if (julieVisionFollow != null)
        {
            julieVisionFollow.enabled = false;
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

        if (crosshair != null)
        {
            crosshair.enabled = true;
        }

        if (toolbar != null)
        {
            toolbar.SetActive(true);
        }

        if (julieMovement != null)
        {
            julieMovement.enabled = true; 
        }

        if (julieVisionFollow != null)
        {
            julieVisionFollow.enabled = true; 
        }
    }
}
