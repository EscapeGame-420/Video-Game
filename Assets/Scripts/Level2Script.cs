using System.Collections;
using UnityEngine;
using TMPro;

public class CollisionTextEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float textDisplayDuration = 5.0f;

    private void Start()
    {
        DisableTextMeshPro();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with this object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enable the TextMeshPro component when the player collides
            EnableTextMeshPro();
            StartCoroutine(DisableTextAfterDelay());

            // Call FadeIn() from FadeOutEffect
        }
    }

    private IEnumerator DisableTextAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Disable the TextMeshPro component after the delay
        DisableTextMeshPro();
    }

    public void EnableTextMeshPro()
    {
        if (textMeshPro != null)
        {
            textMeshPro.enabled = true;
        }
    }

    public void DisableTextMeshPro()
    {
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }
}
