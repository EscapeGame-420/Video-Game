using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    public Transform player;
    public float activationDistance;
    public bool isCandleNear = false;
    public bool canvasCreated = false; // Flag to track if the canvas has been created
    public GameObject obstacle;
    public Transform canvasTransform;

    [SerializeField]
    public AudioClip selectionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = selectionSound;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();

        UpdateCanvasVisibility(distance);
        if (CanActivateCanvas(distance, inventory))
        {
            HandleCanvasCreation();
        }

        HandleInteraction(inventory);
    }

    public void UpdateCanvasVisibility(float distance)
    {
        if (canvasCreated && canvasTransform != null)
        {
            canvasTransform.gameObject.SetActive(distance <= activationDistance);
        }
    }

    public bool CanActivateCanvas(float distance, Inventory inventory)
    {
        return distance <= activationDistance && inventory != null && inventory.IncludeItemName("crowbar");
    }

    public void HandleCanvasCreation()
    {
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true;

            canvasTransform = transform.Find(this.gameObject.name+"Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(1.67f, 2.37f, -0.5f);
            }
        }
    }

    public void HandleInteraction(Inventory inventory)
    {
        if (Input.GetKeyDown("e") && inventory.IsSelectingItem("crowbar") && gameObject.transform.childCount > 1)
        {
            if (selectionSound != null)
            {
                audioSource.clip = selectionSound;
                audioSource.Play();
            }
            Destroy(gameObject.transform.GetChild(0).gameObject);
            obstacle.SetActive(true);

            if (gameObject.transform.childCount <= 3)
            {
                Destroy(gameObject);
                Destroy(canvasTransform.gameObject);
                inventory.UseItem("crowbar");
            }
        }
    }
}