using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float activationDistance = 1.5f;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private AudioClip selectionSound;
    private AudioSource audioSource;

    [SerializeField]
    private float canvasHeightOffset = 0f; // Offset for the canvas height
    [SerializeField]
    private float canvasxOffset = 0f; // Offset for the canvas X position
    [SerializeField]
    private float canvaszOffset = -0.1f; // Offset for the canvas Z position

    public string itemName;
    public Sprite sprite;

    void Start()
    {
        canvas = CreateCanvas(this.gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponent<Inventory>();
    }
    

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
            canvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlaySelectionSound();
                inventory.AddItem(this);
                Debug.Log("Item picked up");
                Destroy(gameObject);
            }
        }
        else
        {
            canvas.enabled = false;
        }

        if (canvas != null)
        {
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
            canvasRectTransform.position = transform.position + new Vector3(canvasxOffset, canvasHeightOffset, canvaszOffset);
        }
    }

    public static Canvas CreateCanvas(GameObject itemObject)
    {
        // Create a new canvas object
        GameObject canvasObject = new GameObject(itemObject.name + "Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.dynamicPixelsPerUnit = 10f;

        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(1, 1); // Square canvas
        canvasRectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        canvasRectTransform.position = itemObject.transform.position + new Vector3(0, 0.7f, 0); // Default offset above the item
        canvasObject.AddComponent<LookAtCam>();

        // Attach to the item
        canvasObject.transform.SetParent(itemObject.transform, true);

        // Create a background image
        GameObject imageObject = new GameObject("GrabBackground");
        Image image = imageObject.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // Semi-transparent background

        RectTransform imageRectTransform = imageObject.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = new Vector2(50, 50); // Background size
        imageObject.transform.SetParent(canvasObject.transform, false);

        // Create a text object for the interaction prompt
        GameObject textObject = new GameObject("GrabText");
        TextMeshProUGUI grabText = textObject.AddComponent<TextMeshProUGUI>();
        grabText.text = "E";
        grabText.fontSize = 50;
        grabText.color = Color.white;
        grabText.fontStyle = FontStyles.Bold;
        grabText.alignment = TextAlignmentOptions.Center;

        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(150, 150); // Text size
        textObject.transform.SetParent(canvasObject.transform, false);

        return canvas;
    }

    private void PlaySelectionSound()
    {
        if (selectionSound != null)
        {
            audioSource.clip = selectionSound;
            audioSource.Play();
        }
    }
}
