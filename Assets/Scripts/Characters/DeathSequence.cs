using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DeathSequence : MonoBehaviour
{
    public GameObject deathCanvas;
    public Image blackScreen;
    public JulieMovement julieMovement;
    public JulieVisionFollow julieVisionFollow;
    AudioSource audioSource;
    [SerializeField] float audibleDistance = 15f;
    [SerializeField] AudioClip sndGrowl, sndAttack;

    private bool attackSoundPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = audibleDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        if (deathCanvas != null) deathCanvas.SetActive(false);

        SetImageAlpha(blackScreen, 0);
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    public void TriggerDeathSequence()
    {
        PlayAttackSound();
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
            StartCoroutine(DeathEffectCoroutine());
        }

        if (julieMovement != null) julieMovement.enabled = false;
        if (julieVisionFollow != null) julieVisionFollow.enabled = false;

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void PlayAttackSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.volume = 0.8f;
            audioSource.pitch = 1.5f;
            audioSource.PlayOneShot(sndAttack);
            attackSoundPlayed = true;
        }
    }

    private IEnumerator DeathEffectCoroutine()
    {
        float fadeDuration = 2.0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            SetImageAlpha(blackScreen, Mathf.Lerp(0, 1, elapsedTime / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene("DeathScene");
    }
}
