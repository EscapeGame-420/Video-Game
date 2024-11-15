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
    AudioSource audioSource; // AudioSource reference for NPC sounds
    [SerializeField] float audibleDistance = 15f;
    [SerializeField] AudioClip sndGrowl, sndAttack;
    
    private bool attackSoundPlayed = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>(); // Initialisation du son

        audioSource.spatialBlend = 1.0f; // Set to 3D sound
        audioSource.maxDistance = audibleDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        if (deathCanvas != null) deathCanvas.SetActive(false);

        if (blackScreen != null) {
            Color blackColor = blackScreen.color;
            blackColor.a = 0;
            blackScreen.color = blackColor;
        }

        if (gameOverImage != null) {
            Color imageColor = gameOverImage.color;
            imageColor.a = 0;
            gameOverImage.color = imageColor;
        }
    }

    public void TriggerDeathSequence() {
        PlayAttackSound();
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

    public void PlayAttackSound() {
        if (!audioSource.isPlaying) {
            audioSource.volume = 0.8f;
            audioSource.pitch = 1.5f;
            audioSource.PlayOneShot(sndAttack);
            attackSoundPlayed = true;
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
