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
    public AudioSource audioSource;
    public float audibleDistance = 15f;
    public AudioClip sndGrowl, sndAttack;

    public bool attackSoundPlayed = false;

    public void Start()
    {
        InitializeAudioSource();
        SetInitialCanvasState();
        SetImageAlpha(blackScreen, 0);
    }

    public void InitializeAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = audibleDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    }

    public void SetInitialCanvasState()
    {
        if (deathCanvas != null) deathCanvas.SetActive(false);
    }

    public void SetImageAlpha(Image image, float alpha)
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
        ActivateDeathCanvas();
        DisablePlayerMovementAndVision();
        EnableCursor();
        StartCoroutine(DeathEffectCoroutine());
    }

    public void ActivateDeathCanvas()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
    }

    public void DisablePlayerMovementAndVision()
    {
        if (julieMovement != null) julieMovement.enabled = false;
        if (julieVisionFollow != null) julieVisionFollow.enabled = false;
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void PlayAttackSound()
    {
        if (!audioSource.isPlaying && !attackSoundPlayed)
        {
            audioSource.volume = 0.8f;
            audioSource.pitch = 1.5f;
            audioSource.PlayOneShot(sndAttack);
            attackSoundPlayed = true;
        }
    }

    public IEnumerator DeathEffectCoroutine()
    {
        float fadeDuration = 2.0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            SetImageAlpha(blackScreen, Mathf.Lerp(0, 1, elapsedTime / fadeDuration));
            yield return null;
        }

        LoadDeathScene();
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
