using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItems : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    public float x =0;
    public float y =0;
    public float z =0;
    public Sprite spritetorch;
    public string itemName;
    private Transform canvasTransform; // Reference to the created canvas


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
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
            canvasTransform = transform.Find(this.gameObject.name + "Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(x,y,z); // Set the desired position
            }
        }
        
        
        if (!(distance <= activationDistance && !inventory.IncludeItemName(itemName))){
            canvasTransform.gameObject.SetActive(false);
            return;
        }
        else
        {
            canvasTransform.gameObject.SetActive(true);
        }

        
        Debug.Log("Le joueur s'approche");

        if (Input.GetKeyDown("e") &&  !inventory.IncludeItemName(itemName))
        {
            Item item = new Item // Create NEW instance per slot
            {
                itemName = itemName,
                sprite = spritetorch
            };
            inventory.AddItem(item);
        
        
        }
    }
}