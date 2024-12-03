using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CollisionTextEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Image fadeImage;
    public GameObject fadeOutEffectObject;
    private FadeOutEffect fadeOutEffect;
    public float textDisplayDuration = 5.0f;
    public bool hasFaded=false;

    private void Start()
    {
        fadeOutEffect = FindObjectOfType<FadeOutEffect>();
        InitializeFadeImage();
    }

    public void InitializeFadeImage()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with this object is the player
        if (collision.gameObject.CompareTag("Player")&& !hasFaded)
        {
            if(!hasFaded)
            {
                hasFaded=true;
                if (!hasFaded)
                {
                Debug.LogError("Player collided with the object");
                }
                fadeOutEffect.fadeDuration=2.0f;
                fadeOutEffect.delayBeforeFade=1.0f;
                fadeOutEffect.InitializeFadeImage();

                StartCoroutine(fadeOutEffect.FadeOut2());
                
            }
            // Enable the TextMeshPro component when the player collides

            // Call FadeIn() from FadeOutEffect
        }
    }


}
