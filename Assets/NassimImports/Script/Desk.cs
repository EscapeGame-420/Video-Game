// using System.Collections;
// using UnityEngine;

// public class DeskController : MonoBehaviour
// {
//     [SerializeField] private Transform player; // Player
//     [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
//     [SerializeField] private AudioClip drawerSound; // Sound to play when drawer opens and closes
//     [SerializeField] private Transform drawer; // Drawer
//     [SerializeField] private Vector3 openPositionDrawer = new Vector3(0f, 0f, 0.5f); // Position when drawer is pulled out
//     [SerializeField] private float animationSpeed = 2f; // Speed of the drawer animation

//     private AudioSource audioSource;
//     private bool isOpen = false; // Tracks if the drawer is open
//     private bool isAnimating = false; // Prevents interaction during animation
//     private Vector3 closedDrawerPosition; // Initial drawer position
//     private Vector3 openDrawerPosition; // Drawer position when open

//     private void Start()
//     {
//         // Initialize audio source
//         audioSource = gameObject.AddComponent<AudioSource>();
//         audioSource.playOnAwake = false;

//         // Set positions
//         closedDrawerPosition = drawer.localPosition;
//         openDrawerPosition = closedDrawerPosition + openPositionDrawer;
//     }

//     private void Update()
//     {
//         float distance = Vector3.Distance(transform.position, player.position);

//         if (distance <= activationDistance && !isAnimating && Input.GetKeyDown(KeyCode.E))
//         {
//             AnimateDrawer();
//         }
//     }

//     private void AnimateDrawer()
//     {
//         isAnimating = true;

//         // Play sound
//         if (drawerSound != null)
//         {
//             audioSource.clip = drawerSound;
//             audioSource.Play();
//         }

//         StartCoroutine(MoveDrawer());
//     }

//     private IEnumerator MoveDrawer()
//     {
//         Vector3 startPosition = isOpen ? openDrawerPosition : closedDrawerPosition;
//         Vector3 targetPosition = isOpen ? closedDrawerPosition : openDrawerPosition;

//         float elapsedTime = 0f;
//         while (elapsedTime < 1f)
//         {
//             drawer.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
//             elapsedTime += Time.deltaTime * animationSpeed;
//             yield return null;
//         }

//         drawer.localPosition = targetPosition; // Snap to final position
//         isOpen = !isOpen;
//         isAnimating = false;
//     }
// }





////////////// Problem with position ///////////////////////////
////////////// confusion between 2 types of position ////////////
////////////// may be because of the git merge//////////////

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DeskController : MonoBehaviour
// {
//     [SerializeField] private Transform player; // Player
//     [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
//     [SerializeField] private AudioClip drawerSound; // Sound to play when drawer opens and closes
//     [SerializeField] private Transform drawer; // drawer
//     [SerializeField] private Vector3 openPositionDrawer = new Vector3(0f, 0f, 0.5f); //  position when drawer slided out
//     [SerializeField] private float animationSpeed = 2f; // Speed of the drawer opening animation
//     //[SerializeField] private Canvas interactionCanvas; // Canvas "E"

//     private AudioSource audioSource;
//     private bool isOpen = false;
//     private Vector3 ClosedDrawer;
//     private Vector3 OpenDrawer;
//     private Vector3 targetDrawerPosition;

//     private void Start()
//     {
//         audioSource = gameObject.AddComponent<AudioSource>();
//         audioSource.playOnAwake = false;

//         ClosedDrawer = drawer.localPosition;
//         OpenDrawer = ClosedDrawer + openPositionDrawer;
//         targetDrawerPosition = ClosedDrawer; // return to closed

//         // Canvas at start
//         // if (interactionCanvas == null)
//         // {
//         //     interactionCanvas = CreateCanvas(this.gameObject);
//         // }
//         // interactionCanvas.enabled = false; // Hide initially

//     }

//     private void Update()
//     {
//         float distance = Vector3.Distance(transform.position, player.position);

//         if (distance <= activationDistance)
//         {
//             // interactionCanvas.enabled = true; // show message

//             if (Input.GetKeyDown(KeyCode.E))
//             {
//                 // interactionCanvas.enabled = false; // hide
//                 MoveDrawer(); 
//             }
//         }
//         // else 
//         // {
//         //     interactionCanvas.enabled = false: // hide
//         // }
       
//        // Smoothly move drawe
//        drawer.localPosition = Vector3.Lerp(drawer.localPosition, targetDrawerPosition, Time.deltaTime * animationSpeed);
//     }

//     private void MoveDrawer()
//     {

//         if (isOpen)
//         {
//             // Close drawer
//             transform.position = ClosedDrawer;
//         }
//         else
//         {
//             // Open drawer
//             transform.position = OpenDrawer;

//         }

//         // Change state
//         isOpen = !isOpen;

//         if(audioSource != null)
//         {
//             audioSource.clip = drawerSound;
//             audioSource.Play();
//         }
//     }
// }

using System.Collections;
using UnityEngine;
using TMPro;

public class DrawerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
    [Header("Drawer Settings")]
    [SerializeField] private Transform drawer; // Reference to the drawer
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0f, 0f, 0.35f); // Offset to slide the drawer out
    [SerializeField] private float animationSpeed = 2f; // Speed of drawer animation
    [Header("Audio Settings")]
    [SerializeField] private AudioClip drawerOpenSound; // Sound played when drawer opens
    [SerializeField] private AudioClip drawerCloseSound; // Sound played when drawer closes

    //[Header("UI Settings")]
    //[SerializeField] private Canvas interactionCanvas; // Canvas for interaction prompt

    private AudioSource audioSource;
    private bool isDrawerOpen = false; // Tracks the state of the drawer
    private bool isAnimating = false; // Prevents multiple activations during animation

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
                    StartCoroutine(OpenDrawer());
                }
                else
                {
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
        isAnimating = true;
        isDrawerOpen = true;

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

