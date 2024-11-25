using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoorController : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to player
    [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
    [SerializeField] private AudioClip doorSound; // Sound to play when doors open and close
    [SerializeField] private Transform leftDoor; // Reference Left Door
    [SerializeField] private Transform rightDoor; //  Right Door
    [SerializeField] private Vector3 leftDoorOpenRotation = new Vector3(0, 90, 0); // Open rotation for left door
    [SerializeField] private Vector3 rightDoorOpenRotation = new Vector3(0, -90, 0); // Open rotation for  right door
    [SerializeField] private float animationSpeed = 2f; // Speed of the door opening animation
    // [SerializeField] private Canvas canvas; // Interaction prompt canvas

    private AudioSource audioSource;
    private bool isOpen = false;
    // Quaternion to use rotation values, in degrees
    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;
    private Quaternion leftDoorTargetRotation;
    private Quaternion rightDoorTargetRotation;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // initial closed rotations
        leftDoorClosedRotation = leftDoor.rotation;
        rightDoorClosedRotation = rightDoor.rotation;

        // Set target rotations to initial rotations
        leftDoorTargetRotation = leftDoorClosedRotation;
        rightDoorTargetRotation = rightDoorClosedRotation;

        // if (canvas == null)
        // {
        //     canvas = CreateCanvas(this.gameObject);
        // }

        // canvas.enabled = false; // Hide the interaction prompt initially

        // AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = doorSound;

    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && Input.GetKeyDown(KeyCode.E))
        {
            // canvas.enabled = true; // Show interaction prompt

            AnimateDoors();

            // Display prompt or other UI if needed
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     // canvas.enabled = false; // Hide canvas during interaction
            //     if (!isDoorOpen)
            //     {
            //         StartCoroutine(OpenDoor());
            //     }
            //     else
            //     {
            //         StartCoroutine(CloseDoor());
            //     }
            // }
        }
        // Smoothly rotate the door toward the target rotation
        leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorTargetRotation, Time.deltaTime * animationSpeed);
        rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorTargetRotation, Time.deltaTime * animationSpeed);

    }

    private void AnimateDoors()
    {
        if (isOpen)
        {
            // Close the doors
            leftDoorTargetRotation = leftDoorClosedRotation;
            rightDoorTargetRotation = rightDoorClosedRotation;
        }
        else
        {
            // Open the doors
            leftDoorTargetRotation = leftDoorClosedRotation * Quaternion.Euler(leftDoorOpenRotation);
            rightDoorTargetRotation = rightDoorClosedRotation * Quaternion.Euler(rightDoorOpenRotation);
        }

        isOpen = !isOpen;

        if(audioSource != null && doorSound != null)
        {
            audioSource.Play();
        }
    }




    ////////////////////// code to control doors separatly ////////////////

    // private IEnumerator OpenDoor()
    // {
    //     isAnimating = true;

    //     if (doorOpenSound != null)
    //     {
    //         audioSource.clip = doorOpenSound;
    //         audioSource.Play();
    //     }

    //     // For rotation when door opens
    //     Quaternion originalRotation = doorTransform.rotation;
    //     Quaternion targetRotation = originalRotation * Quaternion.Euler(openRotation);
    //     float elapsedTime = 0f;

    //     while (elapsedTime < 1f)
    //     {
    //         // ensure smooth rotation
    //         float progress = Mathf.Clamp01(elapsedTime); // Math.Clamp01(not necessary)  return value between 0 and 1
    //         doorTransform.rotation = Quaternion.Lerp(originalRotation, targetRotation, progress);
    //         elapsedTime += Time.deltaTime * animationSpeed;
    //         yield return null; // yield : used with IEnumerator to provide a way to pause and resume execution in a method

    //     }

    //     doorTransform.rotation = targetRotation; // Ensure final position
    //     isDoorOpen = true;
    //     isAnimating = false;
    // }

    // private IEnumerator CloseDoor()
    // {
    //     isAnimating = true;

    //     Quaternion originalRotation = doorTransform.rotation;
    //     Quaternion targetRotation = originalRotation * Quaternion.Euler(-openRotation);
    //     float elapsedTime = 0f;

    //     while (elapsedTime < 1f)
    //     {
    //         float progress = Mathf.Clamp01(elapsedTime);
    //         doorTransform.rotation = Quaternion.Lerp(originalRotation, targetRotation, progress);
    //         elapsedTime += Time.deltaTime * animationSpeed;
    //         yield return null;
    //     }

    //     doorTransform.rotation = targetRotation; // Ensure final position
    //     isDoorOpen = false;
    //     isAnimating = false;
    // }



    // public static Canvas CreateCanvas(GameObject wardrobeObject)
    // {
    //     // Create and configure the canvas
    //     GameObject canvasObject = new GameObject(wardrobeObject.name + "Canvas");
    //     canvasObject.AddComponent<Canvas>();
    //     canvasObject.AddComponent<CanvasScaler>();
    //     canvasObject.AddComponent<GraphicRaycaster>();

    //     Canvas canvas = canvasObject.GetComponent<Canvas>();
    //     canvas.renderMode = RenderMode.WorldSpace;
    //     canvas.enabled = false;

    //     RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
    //     canvasRectTransform.sizeDelta = new Vector2(200, 100);
    //     canvasObject.transform.SetParent(wardrobeObject.transform, false);

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
