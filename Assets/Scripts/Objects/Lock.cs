using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    public string itemName;
    public GameObject lockObject;
    public float x =0.7f;
    public float y =1.2f;
    public float z =-0.2f;
    private Transform canvasTransform; // Reference to the created canvas

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        foreach (Transform child in transform){
            if (child.name == "Padlock"){
                lockObject = child.gameObject;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (!(distance <= activationDistance && inventory.IncludeItemName(itemName))) return;


        // Check if the canvas has already been created
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; 
            canvasTransform = transform.Find(this.gameObject.name + "Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(x,y,z); // Set the desired position
            }
        }

        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && inventory.IsSelectingItem(itemName))
        {
            Destroy(lockObject);
            Destroy(canvasTransform.gameObject);

            inventory.UseItem(itemName);
            
            Door door = GameObject.Find("DoorDouble").GetComponent<Door>();
            door.OpenLock();

        }
    }
}