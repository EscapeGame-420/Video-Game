using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOutEffect : MonoBehaviour
{
    public Image fadeImage;
    public Image crosshair;
    public GameObject toolbar;
    public JulieMovement julieMovement;
    public JulieVisionFollow julieVisionFollow;
    public TextMeshProUGUI textMeshPro;
    public float fadeDuration = 2.0f;
    public float delayBeforeFade = 1.0f;

    private float elapsedTime = 0f;

    public void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        InitializeFadeImage();
        HideCrosshair();
        HideToolbar();
        DisableMovementAndVision();
        DisableTextMeshPro();
        StartCoroutine(FadeOut());
    }

    public void InitializeFadeImage()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    public void HideCrosshair()
    {
        if (crosshair != null)
        {
            crosshair.enabled = false;
        }
    }

    public void HideToolbar()
    {
        if (toolbar != null)
        {
            toolbar.SetActive(false);
        }
    }

    public void DisableMovementAndVision()
    {
        if (julieMovement != null)
        {
            julieMovement.enabled = false;
        }

        if (julieVisionFollow != null)
        {
            julieVisionFollow.enabled = false;
        }
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alphaValue);
            yield return null;
        }

        EnableCrosshair();
        ShowToolbar();
        EnableMovementAndVision();

        EnableTextMeshPro();
        yield return new WaitForSeconds(2.0f);
        DisableTextMeshPro();
    }

    public void EnableCrosshair()
    {
        if (crosshair != null)
        {
            crosshair.enabled = true;
        }
    }

    public void ShowToolbar()
    {
        if (toolbar != null)
        {
            toolbar.SetActive(true);
        }
    }

public IEnumerator FadeOut2()
{
    elapsedTime = 0f; // Reset elapsedTime at the beginning

    yield return new WaitForSeconds(delayBeforeFade);

    while (elapsedTime < fadeDuration)
    {
        elapsedTime += Time.deltaTime;
        float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
        fadeImage.color = new Color(0f, 0f, 0f, alphaValue);
        yield return null;
    }

    EnableCrosshair();
    ShowToolbar();
    EnableMovementAndVision();

    EnableTextMeshPro();
    yield return new WaitForSeconds(2.0f);
    DisableTextMeshPro();
}
    public void EnableMovementAndVision()
    {
        if (julieMovement != null)
        {
            julieMovement.enabled = true;
        }

        if (julieVisionFollow != null)
        {
            julieVisionFollow.enabled = true;
        }
    }

    public void DisableTextMeshPro()
    {
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }

    public void EnableTextMeshPro()
    {
        if (textMeshPro != null)
        {
            textMeshPro.enabled = true;
        }
    }
}
