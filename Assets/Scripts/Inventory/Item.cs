using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField]
    public Inventory inventory;
    [SerializeField]
    public Transform player;
    [SerializeField]
    public float activationDistance = 1.5f;
    [SerializeField]
    public Canvas canvas;
    [SerializeField]
    public AudioClip selectionSound;
    public AudioSource audioSource;

    [SerializeField]
    public float canvasHeightOffset = 0f;
    [SerializeField]
    public float canvasxOffset = 0f;
    [SerializeField]
    public float canvaszOffset = -0.1f;

    public string itemName;
    public Sprite sprite;

    void Start()
    {
        InitializeItem();
    }

    void Update()
    {
        HandleCanvasVisibility();
        HandleItemPickup();
        UpdateCanvasPosition();
    }

    public void InitializeItem()
    {
        canvas = CreateCanvas(this.gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponent<Inventory>();
    }

    public void HandleCanvasVisibility()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        canvas.enabled = distance <= activationDistance;
    }

    public void HandleItemPickup()
    {
        if (!canvas.enabled || !Input.GetKeyDown(KeyCode.E))
            return;

        PlaySelectionSound();
        inventory.AddItem(this);
        Debug.Log("Item picked up");
        Destroy(gameObject);
    }

    public void UpdateCanvasPosition()
    {
        if (canvas == null) 
            return;

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasRectTransform.position = transform.position + new Vector3(canvasxOffset, canvasHeightOffset, canvaszOffset);
    }

    public static Canvas CreateCanvas(GameObject itemObject)
    {
        GameObject canvasObject = new GameObject(itemObject.name + "Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.dynamicPixelsPerUnit = 10f;

        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(1, 1);
        canvasRectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        canvasRectTransform.position = itemObject.transform.position + new Vector3(0, 0.7f, 0);
        canvasObject.AddComponent<LookAtCam>();

        canvasObject.transform.SetParent(itemObject.transform, true);

        AddCanvasBackground(canvasObject);
        AddCanvasText(canvasObject);

        return canvas;
    }

    public static void AddCanvasBackground(GameObject canvasObject)
    {
        GameObject imageObject = new GameObject("GrabBackground");
        Image image = imageObject.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        RectTransform imageRectTransform = imageObject.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = new Vector2(50, 50);
        imageObject.transform.SetParent(canvasObject.transform, false);
    }

    public static void AddCanvasText(GameObject canvasObject)
    {
        GameObject textObject = new GameObject("GrabText");
        TextMeshProUGUI grabText = textObject.AddComponent<TextMeshProUGUI>();
        grabText.text = "E";
        grabText.fontSize = 50;
        grabText.color = Color.white;
        grabText.fontStyle = FontStyles.Bold;
        grabText.alignment = TextAlignmentOptions.Center;

        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(150, 150);
        textObject.transform.SetParent(canvasObject.transform, false);
    }

    public void PlaySelectionSound()
    {
        if (selectionSound == null)
            return;

        audioSource.clip = selectionSound;
        audioSource.Play();
    }
}
