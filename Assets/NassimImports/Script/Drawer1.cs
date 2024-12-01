using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer1 : MonoBehaviour
{

    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
    [SerializeField] private Transform drawer; // Reference to the drawer
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0f, 0f, 0.35f); // Offset to slide the drawer out
    [SerializeField] private float animationSpeed = 1f; // Speed of drawer animation
    [SerializeField] private AudioClip drawerOpenSound; // Sound played when drawer opens
    [SerializeField] private AudioClip drawerCloseSound; // Sound played when drawer closes

    private AudioSource audioSource;
    private bool isDrawerOpen = false; // Tracks the state of the drawer
    private bool isAnimating = false; // Prevents multiple activations during animation

    private Vector3 closedPosition;
    private Vector3 openPosition;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Store initial closed and open positions
        closedPosition = drawer.localPosition;
        openPosition = closedPosition + openPositionOffset;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance && !isAnimating)
        {
            //interactionCanvas.enabled = true; // Show interaction prompt

            if (Input.GetKeyDown(KeyCode.R))
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
}
