using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// drawer with the key
public class Drawer2 : MonoBehaviour
{

    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float activationDistance = 2f; // Distance to activate interaction
    [SerializeField] private Transform drawer; // Reference to the drawer
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0f, 0f, 0.35f); // Offset to slide the drawer out
    [SerializeField] private float animationSpeed = 1f; // Speed of drawer animation
    [SerializeField] private AudioClip drawerOpenSound; // Sound played when drawer opens
    [SerializeField] private AudioClip drawerCloseSound; // Sound played when drawer closes
    [SerializeField] private GameObject key; // key that appears when drawer is unlocked


    private AudioSource audioSource;
    public static bool isDrawerOpen = false; // Tracks the state of the drawer // static because when not used didn't work when called from other script
    private bool isAnimating = false; // Prevents multiple activations during animation
    private bool isLocked = true; // is drawer locked(at the bigining)

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

        // key is hidden initially(cannot be accessed while the drawer is still closed)
        if (key != null)
        {
            key.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!WardrobeDoorController.doorsOpen) return; // can't open drawer when wardrobe doors are closed

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance && !isAnimating)
        {
            //interactionCanvas.enabled = true; // Show interaction prompt

            if (Input.GetKeyDown(KeyCode.T))
            {
                if(isLocked)
                {
                    //interactionCanvas.enabled = false; // Hide prompt during interaction
 
                    Debug.Log("This drawer is locked. Find the required item to unlock it!");
                }
                else
                {
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
    }

    public void UnlockDrawer()
    {
        isLocked = false;
        Debug.Log("This drawer is now unlocked!");
    }


    public IEnumerator OpenDrawer()
    {
        if(isLocked || isAnimating || isDrawerOpen ) yield break; // if one of these situations is true, gets out of this function with yield break

        // when drawer opens, key isn't hidden anymore
        if (key != null)
        {
            key.SetActive(true);
        }

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
            drawer.localPosition = Vector3.Lerp(closedPosition, openPosition, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Ensure drawer reaches exact open position
        drawer.localPosition = openPosition;
        isAnimating = false;
    }

    public IEnumerator CloseDrawer()
    {
        if(isLocked || isAnimating || !isDrawerOpen ) yield break; // if one of these situations is true, gets out of this function with yield break

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
            drawer.localPosition = Vector3.Lerp(openPosition, closedPosition, elapsedTime);
            elapsedTime += Time.deltaTime * animationSpeed;
            yield return null;
        }

        // Ensure drawer reaches exact closed position
        drawer.localPosition = closedPosition;
        isAnimating = false;
    }
}
