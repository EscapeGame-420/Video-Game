using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItems : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    public bool canvasCreated = false; // Flag to track if the canvas has been created
    public float x =0;
    public float y =0;
    public float z =0;
    public Sprite spritetorch;
    public string itemName;
    public Transform canvasTransform; // Reference to the created canvas


    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        // Check if the canvas has already been created
        HandleCanvasCreation();
        
        
        if (!CanActivateCanvas(distance, inventory)){
            canvasTransform.gameObject.SetActive(false);
            return;
        }
        else
        {
            canvasTransform.gameObject.SetActive(true);
        }

        
        Debug.Log("Le joueur s'approche");
        if (Input.GetKeyDown("e") &&  CanActivateCanvas(distance, inventory))
        {
            HandleInteraction(inventory);
        }
    }
    public bool CanActivateCanvas(float distance, Inventory inventory)
    {
        return distance <= activationDistance && inventory != null && !inventory.IncludeItemName(itemName);
    }
    public void HandleCanvasCreation()
    {
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true;

            canvasTransform = transform.Find(this.gameObject.name + "Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(x, y, z); // Set the desired position
            }
        }
    }
    public void HandleInteraction(Inventory inventory)
    {
    
        Item item = new Item // Create NEW instance per slot
        {
                itemName = itemName,
                sprite = spritetorch
        };
        inventory.AddItem(item);
        
    }
}