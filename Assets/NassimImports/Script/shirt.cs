using System.Collections;
using UnityEngine;

public class Shirt : MonoBehaviour
{
    [SerializeField] private Transform player; // Player transform
    [SerializeField] private float activationDistance = 1.5f; // Distance to activate interaction
    [SerializeField] private AudioClip shirtPullSound; // Sound for pulling the shirt
    [SerializeField] private Transform pulledOutPosition; // Position for shirt when pulled out
    [SerializeField] private float animationSpeed = 2f; // Speed of shirt/drawer movement
    [SerializeField] private Drawer2 drawer2; // Reference to Drawer2 script to unlock it

    private AudioSource audioSource;
    private bool shirtActivated = false; // if shirt has been activated

    private void Start()
    {
        // Initialize audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // See if in activation distance
        if (distance <= activationDistance && !shirtActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(PullOutShirt());
            }
        }
    }

    private IEnumerator PullOutShirt()
    {
        shirtActivated = true;

        // Play pull sound (if available)
        if (shirtPullSound != null)
        {
            audioSource.clip = shirtPullSound;
            audioSource.Play();
        }

        // Pull shirt out
        Vector3 originalShirtPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(originalShirtPosition, pulledOutPosition.position, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Ensure shirt reaches pulled-out position
        transform.position = pulledOutPosition.position;

        // pull shirt back to initial position
        elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(pulledOutPosition.position, originalShirtPosition, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        transform.position = originalShirtPosition;

        // Unlock and Move the drawer to reveal the key
        if(drawer2 != null)
        {
            drawer2.UnlockDrawer();
            StartCoroutine(drawer2.OpenDrawer());
        }


    }

    // private IEnumerator MoveDrawer()
    // {
    //     if (drawerMoved) yield break;

    //     drawerMoved = true; // Prevent multiple activations

    //     Vector3 startDrawerPosition = drawer.transform.position;

    //     while (Vector3.Distance(drawer.transform.position, revealDrawerPosition.position) > 0.01f)
    //     {
    //         drawer.transform.position = Vector3.MoveTowards(
    //             drawer.transform.position,
    //             revealDrawerPosition.position,
    //             animationSpeed * Time.deltaTime
    //         );
    //         yield return null;
    //     }

    //     // Ensure the drawer reaches the exact reveal position
    //     drawer.transform.position = revealDrawerPosition.position;

    //     // Unlock Drawer2
    //     if (drawer2 != null)
    //     {
    //         drawer2.UnlockDrawer();
    //     }

    //     Debug.Log("Drawer opened to reveal the key!");
    // }
}
