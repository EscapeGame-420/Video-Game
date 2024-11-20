using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretRoomActivator : MonoBehaviour
{
    [SerializeField] private Transform player; // Player transform
    [SerializeField] private float activationDistance = 1.5f; // Distance to activate interaction
    //[SerializeField] private Canvas canvas; // UI Canvas for interaction prompt, not used for now
    [SerializeField] private AudioClip bookPullSound; // Sound for pulling the book
    [SerializeField] private GameObject libraryShelf; // The shelf GameObject
    [SerializeField] private Transform pulledOutPosition; // Position for the book when pulled out
    [SerializeField] private Transform originalShelfPosition; // Original position of the shelf
    [SerializeField] private Transform revealShelfPosition; // Position to reveal the secret room, when door is moved
    [SerializeField] private float animationSpeed = 2f; // Speed of book/shelf movement
    [SerializeField] private Vector3 backwardRotation = new Vector3(0f, 0f, -15f); //  rotation to incline the book backward when pulled


    private AudioSource audioSource;
    private bool bookActivated = false;
    private bool shelfRevealed = false;

    private void Start()
    {
        //canvas = CreateCanvas(this.gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && !bookActivated)
        {
            //canvas.enabled = true; // Show interaction prompt

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ActivateSecretRoom());
            }
        }
        // else
        // {
        //     canvas.enabled = false; // Hide interaction prompt
        // }
    }

    private IEnumerator ActivateSecretRoom()
    {
        bookActivated = true;
        //canvas.enabled = false;

        // Play sound (if available)
        if (bookPullSound != null)
        {
            audioSource.clip = bookPullSound;
            audioSource.Play();
        }

        // Pull the book out with rotation backward
        Vector3 originalBookPosition = transform.position;
        Quaternion originalBookRotation = transform.rotation; // Original rotation taken by Quaternion
        Quaternion targetRotation = originalBookRotation * Quaternion.Euler(backwardRotation); // target rotation, Quaternion.Euler takes 3 floats in degrees
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float progress = elapsedTime;
            // Lerp object position(linear interpolation), smoothly transition the position of an object 
            // from startPosition to  targetPosition based on the progress value. (0= startPosition, 1 = targetPosition)
            transform.position = Vector3.Lerp(originalBookPosition, pulledOutPosition.position, elapsedTime);
            transform.rotation = Quaternion.Lerp(originalBookRotation, targetRotation, progress); // smooth rotation effect
            elapsedTime += Time.deltaTime * animationSpeed;
            // yield : used with IEnumerator to provide a way to pause and resume execution in a method
            yield return null;
        }

        // Return the book
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            float progress = elapsedTime;
            transform.position = Vector3.Lerp(pulledOutPosition.position, originalBookPosition, elapsedTime);
            transform.rotation = Quaternion.Lerp(targetRotation, originalBookRotation, progress);
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
            Debug.Log(Vector3.Distance(libraryShelf.transform.position, revealShelfPosition.position));
            // Move the shelf closer to the target position
            libraryShelf.transform.position = Vector3.MoveTowards(
                libraryShelf.transform.position,
                revealShelfPosition.position, 
                animationSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Snap the shelf to the exact reveal position to ensure precision, (not necessary)
        libraryShelf.transform.position = revealShelfPosition.position;

        // Mark shelf as Revealed (Moved)
        shelfRevealed = true;
        Debug.Log("Secret room revealed!");
    }

    // public static Canvas CreateCanvas(GameObject bookObject)
    // {
    //     // Create and configure the canvas
    //     GameObject canvasObject = new GameObject(bookObject.name + "Canvas");
    //     canvasObject.AddComponent<Canvas>();
    //     canvasObject.AddComponent<CanvasScaler>();
    //     canvasObject.AddComponent<GraphicRaycaster>();

    //     Canvas canvas = canvasObject.GetComponent<Canvas>();
    //     canvas.renderMode = RenderMode.WorldSpace;
    //     canvas.enabled = false;

    //     RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
    //     canvasRectTransform.sizeDelta = new Vector2(200, 100);
    //     canvasObject.transform.SetParent(bookObject.transform, false);

    //     // Create the prompt text
    //     GameObject textObject = new GameObject("InteractionText");
    //     TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
    //     text.text = "Press E";
    //     text.fontSize = 24;
    //     text.alignment = TextAlignmentOptions.Center;
    //     text.color = Color.white;

    //     RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
    //     textRectTransform.sizeDelta = new Vector2(200, 50);
    //     textRectTransform.SetParent(canvasObject.transform, false);

    //     return canvas;
    // }
}




// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class SecretRoomActivator : MonoBehaviour
// {
//     [SerializeField] private Transform player; // Player transform
//     [SerializeField] private float activationDistance = 1.5f; // Distance to activate interaction
//     //[SerializeField] private Canvas canvas; // UI Canvas for interaction prompt, not used for now
//     [SerializeField] private AudioClip bookPullSound; // Sound for pulling the book
//     [SerializeField] private GameObject libraryShelf; // The shelf GameObject
//     [SerializeField] private Transform pulledOutPosition; // Position for the book when pulled out
//     [SerializeField] private Transform originalShelfPosition; // Original position of the shelf
//     [SerializeField] private Transform revealShelfPosition; // Position to reveal the secret room
//     [SerializeField] private float animationSpeed = 2f; // Speed of book/shelf movement

//     private AudioSource audioSource;
//     private bool isPlayerInRange = false;  //V2
//     private bool bookActivated = false;
//     private bool shelfRevealed = false;

//     // V2
//     private float elapsedTime = 0; // timer for animations
//     private Vector3 startPosition; // for animation
//     private Vector3 targetPosition; // for animation
//     private bool isAnimating = false; // is animation ongoing

//     private void Start()
//     {
//         //canvas = CreateCanvas(this.gameObject);
//         audioSource = gameObject.AddComponent<AudioSource>();
//         audioSource.playOnAwake = false;
//     }

//     private void Update()
//     {
//         float distance = Vector3.Distance(transform.position, player.position);
//         isPlayerInRange = distance <= activationDistance;

//         // Show or hide interaction prompt
//         //canvas.enabled = isPlayerInRange && !bookActivated;

//         // book activation
//         if (isPlayerInRange && !bookActivated && Input.GetKeyDown(KeyCode.E))
//         {
//             ActivateBook();
//         }

//         // Animation
//         if(isAnimating)
//         {
//             AnimateObject();
//         }
//     }

//     private void ActivateBook()
//     {
//         bookActivated = true;
//         //canvas.enabled = false;

//         // Play sound (if available), not for the moment
//         if (bookPullSound != null)
//         {
//             audioSource.clip = bookPullSound;
//             audioSource.Play();
//         }

//         // Pull the book out Animation
//         StartAnimation(transform.position, pulledOutPosition.position, () => 
//         {
//             // return book to starting position
//             StartAnimation(pulledOutPosition.position, transform.position, () =>
//             {
//                 // move shelf animation
//                 MoveShelf();
//             });
//         });
//     }

//     private void MoveShelf()
//     {
//         StartAnimation(libraryShelf.transform.position,revealShelfPosition.position, () =>
//         {
//             shelfRevealed = true;
//             Debug.Log("Secret room revealed!");
//         });
//     }

//     private void StartAnimation(Vector3 from, Vector3 to, System.Action onComplete)
//     {
//         startPosition = from;
//         targetPosition = to;
//         elapsedTime = 0f;
//         isAnimating = true;

//         // when animation completed, callback
//         animationCompleteCallback = onComplete;
//     }

//     private void AnimateObject()
//     {
//         // Incrementing time to ensure smooth transition between the frames of the animation
//         elapsedTime += Time.deltaTime * animationSpeed;
//         // Mathf.Clam01(elapsedTime) puts the value of elapsedTime between 0 and 1, 
//         // to ensure the process doesn't exceed the valid range 
//         float progress = Mathf.Clamp01(elapsedTime);

//         // Lerp object position(linear interpolation), smoothly transition the position of an object 
//         // from startPosition to  targetPosition based on the progress value. (0= startPosition, 1 = targetPosition)
//         if (bookActivated && !shelfRevealed)
//         {
//             transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
//         }
//         else
//         {
//             libraryShelf.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
//         }
    
//         // check if animation is complete
//         if (progress >= 1f)
//         {
//             isAnimating = false;

//             // Invoke animation completion callback
//             animationCompleteCallback?.Invoke();
//             animationCompleteCallback = null; // clear callback
//         }
//     }

//     private System.Action animationCompleteCallback; // callback when animation finishes



//         // not used for now
//     // public static Canvas CreateCanvas(GameObject bookObject)
//     // {
//     //     // Create and configure the canvas
//     //     GameObject canvasObject = new GameObject(bookObject.name + "Canvas");
//     //     canvasObject.AddComponent<Canvas>();
//     //     canvasObject.AddComponent<CanvasScaler>();
//     //     canvasObject.AddComponent<GraphicRaycaster>();

//     //     Canvas canvas = canvasObject.GetComponent<Canvas>();
//     //     canvas.renderMode = RenderMode.WorldSpace;
//     //     canvas.enabled = false;

//     //     RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
//     //     canvasRectTransform.sizeDelta = new Vector2(200, 100);
//     //     canvasObject.transform.SetParent(bookObject.transform, false);

//     //     // Create the prompt text
//     //     GameObject textObject = new GameObject("InteractionText");
//     //     TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
//     //     text.text = "Press E";
//     //     text.fontSize = 24;
//     //     text.alignment = TextAlignmentOptions.Center;
//     //     text.color = Color.white;

//     //     RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
//     //     textRectTransform.sizeDelta = new Vector2(200, 50);
//     //     textRectTransform.SetParent(canvasObject.transform, false);

//     //     return canvas;
//     // }
// }
