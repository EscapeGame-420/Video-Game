using UnityEngine;
using TMPro;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public TextMeshProUGUI youDiedText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI backToMenuText;

    public float fadeDuration = 2.0f;
    public float delayBeforeYouDied = 1.0f;

    public void Start()
    {
        InitializeTextAlpha();
        StartCoroutine(FadeInTextSequence());
    }

    public void InitializeTextAlpha()
    {
        SetTextAlpha(youDiedText, 0);
        SetTextAlpha(restartText, 0);
        SetTextAlpha(backToMenuText, 0);
    }

    public IEnumerator FadeInTextSequence()
    {
        yield return new WaitForSeconds(delayBeforeYouDied);

        yield return FadeInText(youDiedText);
        yield return FadeInText(restartText);
        yield return FadeInText(backToMenuText);
    }

    public IEnumerator FadeInText(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            SetTextAlpha(text, alpha);
            yield return null;
        }

        SetTextAlpha(text, 1f);
    }

    public void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}
