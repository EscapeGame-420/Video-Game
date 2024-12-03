using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTextOnStart : MonoBehaviour
{
    public TextMeshProUGUI youDiedText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI backToMenuText;

    public float fadeDuration = 2.0f;
    public float delayBeforeYouDied = 1.0f;

    private void Start()
    {
        SetTextAlpha(youDiedText, 0);
        SetTextAlpha(restartText, 0);
        SetTextAlpha(backToMenuText, 0);

        StartCoroutine(FadeInTextSequence());
    }

    private IEnumerator FadeInTextSequence()
    {
        yield return new WaitForSeconds(delayBeforeYouDied);
        yield return StartCoroutine(FadeInText(youDiedText));
        yield return StartCoroutine(FadeInText(restartText));
        yield return StartCoroutine(FadeInText(backToMenuText));
    }

    private IEnumerator FadeInText(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            SetTextAlpha(text, Mathf.Lerp(0, 1, elapsedTime / fadeDuration));
            yield return null;
        }

        SetTextAlpha(text, 1f);
    }

    private void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}
