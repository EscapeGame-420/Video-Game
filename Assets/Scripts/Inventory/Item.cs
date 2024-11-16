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
    private float canvasHeightOffset= 0f; // Exposed height offset for canvas
    [SerializeField] 
    private float canvasxOffset = 0f; // Exposed height offset for canvas
    [SerializeField] 
    private float canvaszOffset = -0.1f; // Exposed height offset for canvas

    public string itemName;
    public Sprite sprite;
    
    void Start()
    {
        canvas = createCanvas(this.gameObject);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
            canvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))  // Use KeyCode for better readability
            {
                inventory.AddItem(this);
                Debug.Log("Item picked up");
                Destroy(gameObject);
            }
        }
        else
        {
            canvas.enabled = false;
        }

        // Update the canvas position each frame based on the offset
        if (canvas != null)
        {
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
            // Use canvasHeightOffset here
            canvasRectTransform.position = transform.position + new Vector3(canvasxOffset, canvasHeightOffset, canvaszOffset);
        }
    }

    public static Canvas createCanvas(GameObject itemObject)
    {
        // Create a new canvas object
        GameObject canvasObject = new GameObject(itemObject.name + "Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.dynamicPixelsPerUnit = 10f;

        // Set the canvas RectTransform properties
        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(1, 1); // Square canvas
        canvasRectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f); // Scale it down to world space
        // Initial position with a default offset (0.7f)
        canvasRectTransform.position = itemObject.transform.position + new Vector3(0, 0.7f, 0); // Default position above the item

        // Set the canvas rotation to always face upright (world space)
        canvasRectTransform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation

        // Make the canvas a child of the itemObject
        canvasObject.transform.SetParent(itemObject.transform, true);

        // Create a background image for the canvas
        GameObject imageObject = new GameObject("GrabBackground");
        Image image = imageObject.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // Semi-transparent background

        RectTransform imageRectTransform = imageObject.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = new Vector2(50, 50); // Square background
        imageObject.transform.SetParent(canvasObject.transform, false);

        // Create a text object to display "E"
        GameObject textObject = new GameObject("GrabText");
        TextMeshProUGUI grabText = textObject.AddComponent<TextMeshProUGUI>();
        grabText.text = "E";
        grabText.fontSize = 50;
        grabText.color = Color.white;
        grabText.fontStyle = FontStyles.Bold;
        grabText.alignment = TextAlignmentOptions.Center;

        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(150, 150); // Match the square background
        textObject.transform.SetParent(canvasObject.transform, false);

        // Optional: Add a LookAtCam script to make the canvas always face the player
        canvasObject.AddComponent<LookAtCam>();

        return canvas;
    }
}
