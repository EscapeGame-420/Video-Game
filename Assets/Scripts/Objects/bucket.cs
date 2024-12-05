using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bucket : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    public bool canvasCreated = false; // Flag to track if the canvas has been created
    public float x =0;
    public float y =0;
    public float z =0;
    public string itemName;
    public Sprite sprite;
    public string itemName1;
    public Sprite sprite1;
    public Transform canvasTransform; 
    public GameObject goodeffect;
    public GameObject badeffect;

    public List<string> mixItems;
    public GameObject bucketObject;
    public Inventory inventory;
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
            goodeffect.SetActive(false);
            badeffect.SetActive(false);

        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        inventory = FindFirstObjectByType<Inventory>();
        // Check if the canvas has already been created
        HandleCanvasCreation();
        if (CanNotActivateCanvas(distance, inventory))
        {
            canvasTransform.gameObject.SetActive(false);
            return;
        }
        else
        {
            canvasTransform.gameObject.SetActive(true);
        }
        CheckStatus();
        Debug.Log("Le joueur s'approche");
        
        if (Input.GetKeyDown("e") &&  !CanNotActivateCanvas(distance, inventory))
        {
            HandleInteraction(inventory);
        }
        
    }
    public void CheckStatus()
    {
        if (mixItems.Count == 3)
        {
            mixItems.Sort();
            if (string.Join(",", mixItems) == "bones,eyeball,eyeball")
            {
            goodeffect.SetActive(true);
            }
            else
            {
            badeffect.SetActive(true);
            }
        }
        else
        {
            goodeffect.SetActive(false);
            badeffect.SetActive(false);
        }
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
     public bool CanNotActivateCanvas(float distance, Inventory inventory)
    {
    return !(distance <= activationDistance && (inventory.IncludeItemName("bones")|| inventory.IncludeItemName("eyeball")|| mixItems.Count == 3));
    }
    public void HandleInteraction(Inventory inventory)
    {
        if (mixItems.Count == 3)
        {
            mixItems.Sort();
            switch (string.Join(",", mixItems))
            {
                case "bones,eyeball,eyeball":
                    Item item = new Item
                    {
                        itemName = itemName,
                        sprite = sprite
                    };
                    inventory.AddItem(item);
                    bucketObject.SetActive(false);
                    break;
                default:
                    Item defaultItem = new Item
                    {
                        itemName = itemName1,
                        sprite = sprite1
                    };
                    inventory.AddItem(defaultItem);
                    bucketObject.SetActive(false);
                    break;
            }
        }
        else
        {
            if (inventory.IncludeItemName("bones") && inventory.IsSelectingItem("bones"))
            {
                inventory.UseItem("bones");
                mixItems.Add("bones");
            }
            else if (inventory.IncludeItemName("eyeball") && inventory.IsSelectingItem("eyeball"))
            {
                inventory.UseItem("eyeball");
                mixItems.Add("eyeball");
                
            }
        }
    }
    
}