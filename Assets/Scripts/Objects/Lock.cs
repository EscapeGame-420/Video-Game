using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    public bool canvasCreated = false; // Flag to track if the canvas has been created
    public string itemName;
    public GameObject lockObject;
    public float x =0.7f;
    public float y =1.2f;
    public float z =-0.2f;
    public Transform canvasTransform; // Reference to the created canvas

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                player = GameObject.Find("Julie").transform;
            }
        }
        foreach (Transform child in transform){
            if (child.name == "Padlock"){
                lockObject = child.gameObject;
            }
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        UpdateCanvasVisibility(distance);
        if (CanActivateCanvas(distance, inventory))
        {
            HandleCanvasCreation();
        }


        if (Input.GetKeyDown("e") && inventory.IsSelectingItem(itemName) && activationDistance >= distance) 
        {
            Destroy(lockObject);

            inventory.UseItem(itemName);
            
            Door door = FindFirstObjectByType<Door>();
            door.OpenLock();
            Destroy(GameObject.Find(this.gameObject.name+"Canvas"));

        }
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
        return distance <= activationDistance && inventory != null && inventory.IncludeItemName(itemName);
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
                canvasTransform.localPosition = new Vector3(x,y, z);
            }
        }
    }

}