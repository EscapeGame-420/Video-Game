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
    // private bool shelfRevealed = false;

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

    // IEnumerator used to create coroutines(useful for tasks needing to occur over time, like in animations)
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
        // shelfRevealed = true;
        Debug.Log("Secret room revealed!");
    }

}
