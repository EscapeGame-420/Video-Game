using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathSequence : MonoBehaviour
{
    public GameObject deathCanvas;
    public Image blackScreen;
    public Image gameOverImage;
    public JulieMovement julieMovement;
    public JulieVisionFollow julieVisionFollow;
    public GameObject enemy;

    private void Start() {
        if (deathCanvas != null) deathCanvas.SetActive(false);
    }

    public void TriggerDeathSequence() {
        if (deathCanvas != null) {
            deathCanvas.SetActive(true);
            StartCoroutine(DeathEffectCoroutine());
        }

        if (julieMovement != null) {
            julieMovement.enabled = false;
        }

        if (julieVisionFollow != null) {
            julieVisionFollow.enabled = false;
        }
    }

    private IEnumerator DeathEffectCoroutine() {
        float fadeDuration = 2.0f;
        float elapsedTime = 0f;
        Color blackColor = blackScreen.color;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            blackColor.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            blackScreen.color = blackColor;
            yield return null;
        }

        if (enemy != null) {
            enemy.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        elapsedTime = 0f;
        Color imageColor = gameOverImage.color;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            gameOverImage.color = imageColor;
            yield return null;
        }
    }
}
