using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretRoomActivator : MonoBehaviour
{
    [SerializeField] private Transform player; // Player transform
    [SerializeField] private float activationDistance = 1.5f; // Distance to activate interaction
    [SerializeField] private Canvas canvas; // UI Canvas for interaction prompt
    [SerializeField] private AudioClip bookPullSound; // Sound for pulling the book
    [SerializeField] private GameObject libraryShelf; // The shelf GameObject
    [SerializeField] private Transform pulledOutPosition; // Position for the book when pulled out
    [SerializeField] private Transform originalShelfPosition; // Original position of the shelf
    [SerializeField] private Transform revealShelfPosition; // Position to reveal the secret room
    [SerializeField] private float animationSpeed = 2f; // Speed of book/shelf movement

    private AudioSource audioSource;
    private bool bookActivated = false;
    private bool shelfRevealed = false;

    private void Start()
    {
        canvas = CreateCanvas(this.gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && !bookActivated)
        {
            canvas.enabled = true; // Show interaction prompt

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ActivateSecretRoom());
            }
        }
        else
        {
            canvas.enabled = false; // Hide interaction prompt
        }
    }

    private IEnumerator ActivateSecretRoom()
    {
        bookActivated = true;
        canvas.enabled = false;

        // Play sound (if available)
        if (bookPullSound != null)
        {
            audioSource.clip = bookPullSound;
            audioSource.Play();
        }

        // Pull the book out
        Vector3 originalBookPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(originalBookPosition, pulledOutPosition.position, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Return the book
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(pulledOutPosition.position, originalBookPosition, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Move the shelf to reveal the secret room
        StartCoroutine(MoveShelf());
    }

    private IEnumerator MoveShelf()
    {
        Vector3 startShelfPosition = libraryShelf.transform.position;

        while (Vector3.Distance(libraryShelf.transform.position, revealShelfPosition.position) > 0.01f)
        {
            libraryShelf.transform.position = Vector3.MoveTowards(libraryShelf.transform.position, revealShelfPosition.position, animationSpeed * Time.deltaTime);
            yield return null;
        }

        shelfRevealed = true;
    }

    public static Canvas CreateCanvas(GameObject bookObject)
    {
        // Create and configure the canvas
        GameObject canvasObject = new GameObject(bookObject.name + "Canvas");
        canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.enabled = false;

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(200, 100);
        canvasObject.transform.SetParent(bookObject.transform, false);

        // Create the prompt text
        GameObject textObject = new GameObject("InteractionText");
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = "Press E";
        text.fontSize = 24;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(200, 50);
        textRectTransform.SetParent(canvasObject.transform, false);

        return canvas;
    }
}
