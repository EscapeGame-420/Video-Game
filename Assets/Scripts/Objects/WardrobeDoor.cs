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

    public static bool doorsOpen = false; // static to be used in other scripts without problems
    private AudioSource audioSource;
    // Quaternion to use rotation values, in degrees
    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;
    private Quaternion leftDoorTargetRotation;
    private Quaternion rightDoorTargetRotation;

    private void Start()
    {
        // AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = doorSound;

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

    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
            // canvas.enabled = true; // Show interaction prompt

            

            // Display prompt or other UI if needed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // canvas.enabled = false; // Hide canvas during interaction

                AnimateDoors();

            }
        }
        // else
        // {
        //     canvas.enabled =false; // hide if out of range
        // }

        // Smoothly rotate the door toward the target rotation
        leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftDoorTargetRotation, Time.deltaTime * animationSpeed);
        rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightDoorTargetRotation, Time.deltaTime * animationSpeed);

    }

    public void AnimateDoors()
    {
        if(Drawer1.isDrawerOpen || Drawer2.isDrawerOpen) return; // if one of the drawers are open cannot close doors
        if (doorsOpen)
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

        doorsOpen = !doorsOpen;

        if(audioSource != null && doorSound != null)
        {
            audioSource.Play();
        }
    }


    // public static Canvas CreateCanvas(GameObject wardrobeObject)
    // {
    //     // Create and configure the canvas
    //     GameObject canvasObject = new GameObject(wardrobeObject.name + "Canvas");
    //     Canvas canvas = Object.AddComponent<Canvas>();
    //     canvas.renderMode = RenderMode.WorldSpace;

    //     canvasObject.AddComponent<CanvasScaler>();
    //     canvasObject.AddComponent<GraphicRaycaster>();

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
