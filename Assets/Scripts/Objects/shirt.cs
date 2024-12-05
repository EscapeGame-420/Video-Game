using System.Collections;
using UnityEngine;
using TMPro;

public class Shirt : MonoBehaviour
{
    [SerializeField] private Transform player; // Player transform
    [SerializeField] private float activationDistance = 1.5f; // Distance to activate interaction
    [SerializeField] private AudioClip shirtPullSound; // Sound for pulling the shirt
    [SerializeField] private Transform pulledOutPosition; // Position for shirt when pulled out
    [SerializeField] private float animationSpeed = 2f; // Speed of shirt/drawer movement
    [SerializeField] private Drawer2 drawer2; // Reference to Drawer2 script to unlock it
    
    
    //private Canvas canvas; // Canvas to show "F" prompt to let the player understand how to activate the shirt element
    //private TextMeshProUGUI textCanvas; // "F"  for press f
    private AudioSource audioSource;
    private bool shirtActivated = false; // if shirt has been activated
    public GameObject text;
    public GameObject text2;

    private void Start()
    {
        // Initialize audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // create canvas with text
        //CreateCanvas();

        //Don't show text (The answer to the riddle was, indeed, A shirt)
        text.SetActive(false);
        //Show text (This drawer is locked... )
        //text2.SetActive(true);
    }

    private void Update()
    {
        if(!WardrobeDoorController.doorsOpen || !DrawerController.isOpenedOnce) return; // can't open drawer when wardrobe doors are closed

        float distance = Vector3.Distance(transform.position, player.position);

        // See if in activation distance
        if (distance <= activationDistance && !shirtActivated)
        {
            // show canvas
           // ShowCanvas(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                // hide canvas after starting animation
                //ShowCanvas(false);
                StartCoroutine(PullOutShirt());
            }
        }
        //else 
        //{
            // when not in activation distance hide
            //ShowCanvas(false);
        //}
    }

    private IEnumerator PullOutShirt()
    {
        //Hide text (This drawer is locked... )
        text2.SetActive(false);
        //show text (The answer to the riddle was, indeed, A shirt)
        text.SetActive(true);

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

    // private void CreateCanvas()
    // {
    //     // Create Canvas GameObject
    //     GameObject canvasGO = new GameObject("canvas");
    //     canvas = canvasGO.AddComponent<Canvas>();
    //     canvas.renderMode = RenderMode.WorldSpace;

    //     // size and position
    //     RectTransform canvasRect = canvas.GetComponent<RectTransform>();
    //     canvasRect.sizeDelta = new Vector2(200, 100);
    //     canvasRect.position = transform.position + Vector3.up * 1.5f; // position above the shirt

    //     // Create Text GameObject
    //     GameObject textGO = new GameObject("text");
    //     textGO.transform.SetParent(canvasGO.transform);
    //     textCanvas = textGO.AddComponent<TextMeshProUGUI>();

    //     //  configure Text "F" for Press F 
    //     textCanvas.text = "F";
    //     textCanvas.fontSize = 36;
    //     textCanvas.alignment = TextAlignmentOptions.Center;
    //     textCanvas.color = Color.white;

    //     // Set Text RectTransform
    //     RectTransform textRect = textCanvas.GetComponent<RectTransform>();
    //     textRect.sizeDelta = new Vector2(200, 100);
    //     textRect.anchoredPosition = Vector2.zero;

    //     // Hide Canvas initially
    //     canvasGO.SetActive(false);
    // }

    // private void ShowCanvas(bool show)
    // {
    //     if (canvas != null)
    //     {
    //         canvas.gameObject.SetActive(show);
    //     }
    // }
}
