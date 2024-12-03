using System.Collections;
using UnityEngine;
using TMPro;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
    [SerializeField] private Transform drawer; // Reference to the drawer
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0f, 0f, 0.35f); // Offset to slide the drawer out
    [SerializeField] private float animationSpeed = 2f; // Speed of drawer animation
    [SerializeField] private AudioClip drawerOpenSound; // Sound played when drawer opens
    [SerializeField] private AudioClip drawerCloseSound; // Sound played when drawer closes
    //[SerializeField] private Canvas interactionCanvas; // Canvas for interaction prompt

    private AudioSource audioSource;
    private bool isDrawerOpen = false; // Tracks the state of the drawer
    private bool isAnimating = false; // Prevents multiple activations during animation
    public static bool isOpenedOnce = false; // to make sure the player read the enigme before finding the object
    public GameObject text ;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Start()
    {
        // Initialize AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Store initial closed and open positions
        closedPosition = drawer.localPosition;
        openPosition = closedPosition + openPositionOffset;
        text.SetActive(false);

        // Setup Interaction Canvas
        // if (interactionCanvas == null)
        // {
        //     interactionCanvas = CreateCanvas(this.gameObject);
        // }
        // interactionCanvas.enabled = false; // Hide prompt initially
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance && !isAnimating)
        {
            //interactionCanvas.enabled = true; // Show interaction prompt

            if (Input.GetKeyDown(KeyCode.E))
            {
                //interactionCanvas.enabled = false; // Hide prompt during interaction
                if (!isDrawerOpen)
                {
                    // if closed, open
                    StartCoroutine(OpenDrawer());
                }
                else
                {
                    // if open, close
                    StartCoroutine(CloseDrawer());
                }
            }
        }
        // else
        // {
        //     interactionCanvas.enabled = false; // Hide prompt if out of range
        // }
    }

    private IEnumerator OpenDrawer()
    {
        isOpenedOnce = true;
        isAnimating = true;
        isDrawerOpen = true;
        text.SetActive(true);

        // Play drawer open sound
        if (drawerOpenSound != null)
        {
            audioSource.clip = drawerOpenSound;
            audioSource.Play();
        }

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            float progress = Mathf.Clamp01(elapsedTime);
            drawer.localPosition = Vector3.Lerp(closedPosition, openPosition, progress);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Ensure drawer reaches exact open position
        drawer.localPosition = openPosition;
        isAnimating = false;
    }

    private IEnumerator CloseDrawer()
    {
        isAnimating = true;
        isDrawerOpen = false;
        text.SetActive(false);
        // Play drawer close sound
        if (drawerCloseSound != null)
        {
            audioSource.clip = drawerCloseSound;
            audioSource.Play();
        }

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            float progress = Mathf.Clamp01(elapsedTime);
            drawer.localPosition = Vector3.Lerp(openPosition, closedPosition, progress);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Ensure drawer reaches exact closed position
        drawer.localPosition = closedPosition;
        isAnimating = false;
    }

    // private Canvas CreateCanvas(GameObject deskObject)
    // {
    //     // Create and configure the canvas
    //     GameObject canvasObject = new GameObject(deskObject.name + "Canvas");
    //     canvasObject.AddComponent<Canvas>();
    //     canvasObject.AddComponent<CanvasScaler>();
    //     canvasObject.AddComponent<GraphicRaycaster>();

    //     Canvas canvas = canvasObject.GetComponent<Canvas>();
    //     canvas.renderMode = RenderMode.WorldSpace;
    //     canvas.enabled = false;

    //     RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
    //     canvasRectTransform.sizeDelta = new Vector2(200, 100);
    //     canvasObject.transform.SetParent(deskObject.transform, false);

    //     // Create the prompt text
    //     GameObject textObject = new GameObject("InteractionText");
    //     TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
    //     text.text = "Press E";
    //     text.fontSize = 24;
    //     text.alignment = TextAlignmentOptions.Center;
    //     text.color = Color.white;

    //     RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
    //     textRectTransform.sizeDelta = new Vector2(200, 50);
    //     textRectTransform.localPosition = new Vector3(0, 0, 0); // Center the text
    //     textObject.transform.SetParent(canvasObject.transform, false);

    //     return canvas;
    // }
}

